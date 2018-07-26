using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using StudyAPI.Infra.CrossCutting.Helpers;
using StudyAPI.Infra.CrossCutting.Models;
using StudyAPI.Infra.CrossCutting.Services.Email.MandrillEmail;
using StudyAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models.SenderData;
using StudyAPI.Infra.CrossCutting.Services.Interfaces;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Infra.CrossCutting.Services.Email
{
    public class EmailFunctions : IEmailFunctions
    {
        private readonly IMandrillEmailSender _mandrillEmailSender;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IOrganisationRepository _organisationRepository;
        private readonly ICommunicationTemplateRepository _communicationTemplateRepository;
        private readonly IEmailSentRepository _emailSentRepository;
        private readonly IDocumentDownload _documentDownload;
        private readonly IBlobStorage _blobStorage;

        public EmailFunctions(IMandrillEmailSender mandrillEmailSender, IVolunteerRepository volunteerRepository, IOrganisationRepository organisationRepository, ICommunicationTemplateRepository communicationTemplateRepository, IEmailSentRepository emailSentRepository, IDocumentDownload documentDownload, IBlobStorage blobStorage)
        {
            _mandrillEmailSender = mandrillEmailSender;
            _volunteerRepository = volunteerRepository;
            _organisationRepository = organisationRepository;
            _communicationTemplateRepository = communicationTemplateRepository;
            _emailSentRepository = emailSentRepository;
            _documentDownload = documentDownload;
            _blobStorage = blobStorage;
        }
      
        public bool SendEmail(EmailModel emailModel)
        {
            try
            {
                // we need to set LastUpdatedBy_ID for Currently Logged on Volunteer OR Admin   
                var volunteer = _volunteerRepository.GetVolunteer(emailModel.CurrentUserEmail, emailModel.OrganisationId);
                var lastUpdatedBy = volunteer == null ? 0 : volunteer.Volunteer_ID;

                var mandrillKey = ConfigurationManager.AppSettings["Mandrill-Key"];
                var mandrillUserName = ConfigurationManager.AppSettings["Mandrill-UserName"];
                _mandrillEmailSender.SetMandrillEmailSender(mandrillUserName, mandrillKey);
                
                var messageStatus = _mandrillEmailSender.Send(emailModel.MandrillEmailMessage);
                Utilities.EmailMessageStatus emailStatus;

                //create email sent record
                foreach (var emailTo in messageStatus.messageResult)
                {
                    emailStatus = GetEmailStatus(emailTo.status);

                    var emailSent = new EmailSent
                    {
                        Email_VC = emailTo.email,
                        Status_ID = (int) emailStatus,
                        Date_DT = DateTime.Now,
                        Entity_ID = emailModel.EntityId,
                        EntityType_VC = emailModel.EntityType,
                        Message_VC = emailModel.MandrillEmailMessage.html,
                        Organisation_ID = emailModel.OrganisationId,
                        Subject_VC = emailModel.MandrillEmailMessage.subject,
                        Template_ID = emailModel.TemplateId,
                        LastUpdatedBy_ID = lastUpdatedBy,
                        CreateActivity = true,
                        Token_VC = messageStatus.MessageSent ? emailTo._id : null
                    };
                    
                    _emailSentRepository.AddEmailSent(emailSent);
                }

                return messageStatus.MessageSent;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public CommunicationTemplate GetTemplate(string tamplateName, int organisationId, string entityType)
        {
            var template =_communicationTemplateRepository.GetCommunicationTemplate(tamplateName, organisationId, entityType);

            // search for template default
            if (template == null)
            {
                template = _communicationTemplateRepository.GetCommunicationTemplateDefault(tamplateName, entityType);
            }

            return template;
        }

        public async Task<bool> SendVolunteerInviteEmail(Volunteer vol, string token, int orgID, string currentUserEmail, List<KeyValuePair<string, Stream>> Attachments = null)
        {
            var sendEmailToAddress = vol.Email_VC;
            
            var organisation = vol.Organisation ?? _organisationRepository.GetOrganisation(vol.Organisation_ID);
            var organisationName = organisation.Name_VC;

            // get the admin for this organisation
            var orgadmin = _volunteerRepository.GetOrgAdmin(vol.Organisation_ID);
            var orgadminname = orgadmin?.FullName_VC ?? "";
            var orgAdminEmail = orgadmin.Email_VC;

            // send email to registrar to complete the process
            var mail = new MailMessage();
            mail.From = new MailAddress("donotreply@test.com");

            // to and from
            var adminMail = new MailAddress(sendEmailToAddress);
            mail.To.Add(adminMail);
            // set to html
            mail.IsBodyHtml = true;

            // import the template to get the subject line and body
            var subject = "";

            // get the communication template
            string templateName = "InviteVolunteer.html";
            string templateEntityType = "Volunteer";
            var template = GetTemplate(templateName, orgID, templateEntityType);

            // If temaplate not exist doesn't send email
            if (template != null)
            {
                // Get Documents to attachments from tamplate ******
                var templateAttachments = await _documentDownload.GetAttachmentsByTemplateId(template.Template_ID, orgID);

                // import the template from blob storage
                _blobStorage.SetBlobStorage(template.Container_VC);
                var importedTemplate = _blobStorage.GetBlobHtml(template.BlobName_VC);
                
                // substitute the beginning of the URL with test or live account domain
                var domainName = ConfigurationManager.AppSettings["DomainName"];

                var linktocompleteregistration = "<a href=" + domainName + "/Volunteers/VolunteerRegistration?Token=" + token + ">Click here to start the application process now</a>";
                var linkToSiteSales = "<a href=\"http://www.test.com\">test</a>";
                var privacyLink = "<a href=\"https://\">Privacy</a>";
                var legalLink = "<a href=\"https://\">Legal</a>";

                // subject
                subject = template.SubjectLine_VC;
                subject = subject.Replace("[OrgName]", organisationName);
                subject = subject.Replace("[FirstName]", vol.FirstName_VC);
                // and body
                var tempBody = importedTemplate;
                tempBody = tempBody.Replace("[OrgName]", organisationName);
                tempBody = tempBody.Replace("[FirstName]", vol.FirstName_VC);
                tempBody = tempBody.Replace("[LinkToCompleteRegistration]", linktocompleteregistration);
                tempBody = tempBody.Replace("[linkToSiteSales]", linkToSiteSales);
                tempBody = tempBody.Replace("[OrgAdminName]", orgadminname);
                tempBody = tempBody.Replace("[OrgAdminEmail]", orgAdminEmail);

                // Footer of body
                tempBody = tempBody.Replace("[LinkToPrivacy]", privacyLink);
                tempBody = tempBody.Replace("[LinkToLegal]", legalLink);

                mail.Body = tempBody;
                mail.Subject = subject;

                // If have attachments include in the email
                if (Attachments != null)
                {
                    foreach (KeyValuePair<string, Stream> attachment in Attachments)
                    {
                        var mailAttachment = new Attachment(attachment.Value, attachment.Key);
                        mail.Attachments.Add(mailAttachment);
                    }
                }

                if (templateAttachments != null)
                {
                    foreach (KeyValuePair<string, Stream> attachment in templateAttachments)
                    {
                        var mailAttachment = new Attachment(attachment.Value, attachment.Key);
                        mail.Attachments.Add(mailAttachment);
                    }
                }

                var entitytype = "Volunteer";

                var emailModel = new EmailModel
                {
                    MandrillEmailMessage = LoadMandrillMessageFromMailMessage(mail),
                    OrganisationId = orgID,
                    EntityId = vol.Volunteer_ID,
                    TemplateId = template.Template_ID,
                    EntityType = entitytype,
                    CurrentUserEmail = currentUserEmail
                };

                SendEmail(emailModel);

                return true;
            }

            return false;
        }
        
        #region Private Methods

        private MandrillEmailMessage LoadMandrillMessageFromMailMessage(MailMessage message)
        {
            MandrillEmailMessage mandrillMessage = new MandrillEmailMessage();
            mandrillMessage.subject = message.Subject;
            if (message.IsBodyHtml)
                mandrillMessage.html = message.Body;
            else
                mandrillMessage.text = message.Body;
            mandrillMessage.from_email = message.From.Address;
            mandrillMessage.from_name = message.From.DisplayName;
            foreach (var recipient in message.To)
            {
                mandrillMessage.to.Add(new MandrillRecipient { type = "to", email = recipient.Address, name = recipient.DisplayName });
            }
            foreach (var cc in message.CC)
            {
                mandrillMessage.to.Add(new MandrillRecipient { type = "to", email = cc.Address, name = cc.DisplayName });
            }
            mandrillMessage.bcc_address = message.Bcc.Select(b => b.Address).FirstOrDefault();
            mandrillMessage.important = message.Priority == MailPriority.High;
            mandrillMessage.track_opens = true;
            foreach (var attachment in message.Attachments)
            {
                var mandrillAttachment = new MandrillAttachment();
                mandrillAttachment.name = attachment.Name;
                mandrillAttachment.type = attachment.ContentType.MediaType;
                mandrillAttachment.content = Convert.ToBase64String(Utilities.ToByteArray(attachment.ContentStream));

                mandrillMessage.attachments.Add(mandrillAttachment);
            }

            return mandrillMessage;
        }

        private Utilities.EmailMessageStatus GetEmailStatus(string status)
        {
            Utilities.EmailMessageStatus emailStatus;
            switch (status)
            {
                case "sent":
                    emailStatus = Utilities.EmailMessageStatus.Sent;
                    break;
                case "queued":
                    emailStatus = Utilities.EmailMessageStatus.Queued;
                    break;
                case "scheduled":
                    emailStatus = Utilities.EmailMessageStatus.Scheduled;
                    break;
                case "rejected":
                    emailStatus = Utilities.EmailMessageStatus.Rejected;
                    break;
                case "bounced":
                    emailStatus = Utilities.EmailMessageStatus.Failed;
                    break;
                case "soft-bounced":
                    emailStatus = Utilities.EmailMessageStatus.Failed;
                    break;
                case "invalid":
                    emailStatus = Utilities.EmailMessageStatus.Invalid;
                    break;
                default:
                    emailStatus = Utilities.EmailMessageStatus.Failed;
                    break;
            }
            return emailStatus;
        }

        #endregion

    }
}
