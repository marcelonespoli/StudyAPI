using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace ComplyfileAPI.Infra.CrossCutting.Helpers
{
    public static class Utilities
    {
        public enum ErrorPriority
        {
            Low = 1,
            Medium = 2,
            High = 3
        }

        public enum ErrorArea
        {
            Booking = 1,
            Supplier = 2,
            Customer = 3,
            Admin = 4,
            Enquiry = 5,
            Dashboard = 6,
            Tour,
            MyProfile,
            Reports,
            Communications,
            CustomerFeedback,
            BlobStorage,
            CommunicationTemplates,
            Scheduler
        }

        public enum EmailMessageStatus : int
        {
            Queued = -1,
            NotAvailable = 0,
            Sent = 1,
            Opened = 2,
            Scheduled = 3, //sendig
            Rejected = 4,
            Invalid = 5,
            Failed = 6
        };

        public static string getModelErrors(ModelStateDictionary ms)
        {
            var errors = ms.Values.SelectMany(x => x.Errors).ToArray();

            string sRC = "";
            foreach (ModelError e in errors)
                sRC += e.ErrorMessage + "<br />";

            return sRC;
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static Byte[] ToByteArray(System.IO.Stream stream)
        {
            Int32 length = stream.Length > Int32.MaxValue ? Int32.MaxValue : Convert.ToInt32(stream.Length);
            Byte[] buffer = new Byte[length];
            stream.Read(buffer, 0, length);
            return buffer;
        }

        public static string CreateToken(string message, string secret)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageByte = encoding.GetBytes(message);

            HMACSHA256 hmac = new HMACSHA256(keyByte);
            byte[] hashMessageByte = hmac.ComputeHash(messageByte);

            string sbinary = String.Empty;
            for (int i = 0; i < hashMessageByte.Length; i++)
            {
                // Converting to hex, but using lowercase 'x' to get lowercase characters
                sbinary += hashMessageByte[i].ToString("x2");
            }

            return sbinary;
        }


        public static void LogError(string userError, ErrorArea area, ErrorPriority priority, bool userNotified, Exception ex)
        {
            string methodName = new StackFrame(1, true).GetMethod().Name;
            string errPriority = string.Empty;
            errPriority = GetDescription<ErrorPriority>(priority);

            string errArea = string.Empty;
            errArea = GetDescription<ErrorArea>(area);

            StringBuilder sb = new StringBuilder();
            if (ex != null)
            {
                sb.Append(string.Format("Error Message: {0} ", ex.Message));
                if (ex.InnerException != null)
                    sb.Append(string.Format("Inner Exception: {0} ", ex.InnerException));
                sb.Append(string.Format("Stack Trace: {0}", ex.StackTrace));
            }

            //ErrorLogViewModel elvm = new ErrorLogViewModel();
            //elvm.addErrorLog(new ErrorLog()
            //{
            //    DateLogged_DT = DateTime.Now,
            //    UserError_VC = userError,
            //    SystemError_VC = sb.ToString(),
            //    LoggedInUser_ID = WebSecurity.CurrentUserId,
            //});
        }

        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");

            //Tries to find a DescriptionAttribute for a potential friendly name for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }

        //internal static bool SendMail(EmailSettings emailSettings, out string mailError)
        //{
        //    try
        //    {
        //        //Retrieve SMTP settings
        //        //string smtpHost = Complyfile.Configuration.ConfigurationManager.AppSettings["smtpHost"] != null ? Complyfile.Configuration.ConfigurationManager.AppSettings["SMTPHost"] : string.Empty;
        //        //string smtpUser = Complyfile.Configuration.ConfigurationManager.AppSettings["smtpUserEmail"] != null ? Complyfile.Configuration.ConfigurationManager.AppSettings["smtpUserEmail"] : string.Empty;
        //        //string smtpUserName = Complyfile.Configuration.ConfigurationManager.AppSettings["smtpUserName"] != null ? Complyfile.Configuration.ConfigurationManager.AppSettings["smtpUserName"] : string.Empty;
        //        //string smtpPass = Complyfile.Configuration.ConfigurationManager.AppSettings["smtpPassword"] != null ? Complyfile.Configuration.ConfigurationManager.AppSettings["smtpPassword"] : string.Empty;
        //        string mandrillKey = ConfigurationManager<>.AppSettings["Mandrill-Key"];
        //        string mandrillUserName = ConfigurationManager.AppSettings["Mandrill-UserName"];

        //        string fromEmailAddress = emailSettings.FromMailAddress;

        //        //if (!string.IsNullOrWhiteSpace(fromEmailAddress) && emailSettings.EmailCCList != null && !string.IsNullOrEmpty(smtpHost))
        //        if (!string.IsNullOrWhiteSpace(fromEmailAddress) && emailSettings.EmailCCList != null)
        //        {
        //            string[] sMailTo = emailSettings.EmailToList.Split(Convert.ToChar(";"));
        //            MandrillEmailMessage mail = new MandrillEmailMessage();

        //            //Set From and To address
        //            //mail.From = new MailAddress(fromEmailAddress);
        //            mail.from_email = fromEmailAddress;
        //            //foreach (string sAddr in sMailTo)
        //            //{
        //            //    if (sAddr != string.Empty)
        //            //        mail.To.Add(sAddr);
        //            //}
        //            List<MandrillRecipient> mailTo = new List<MandrillRecipient>();
        //            foreach (string sAddr in sMailTo)
        //            {
        //                if (sAddr != string.Empty)
        //                    mailTo.Add(new MandrillRecipient { type = "to", email = sAddr, name = sAddr });
        //            }

        //            //Set CC and Bcc address
        //            //if (emailSettings.EmailCCList != string.Empty)
        //            //    mail.CC.Add(new MailAddress(emailSettings.EmailCCList));
        //            //if (emailSettings.EmailBccList != string.Empty)
        //            //    mail.Bcc.Add(new MailAddress(emailSettings.EmailBccList));

        //            if (emailSettings.EmailCCList != string.Empty)
        //                mailTo.Add(new MandrillRecipient { type = "to", email = emailSettings.EmailCCList, name = emailSettings.EmailCCList });
        //            if (emailSettings.EmailBccList != string.Empty)
        //                mail.bcc_address = emailSettings.EmailBccList;

        //            mail.to = mailTo;

        //            //Set Subject, Body, BodyHtml 
        //            //mail.Subject = emailSettings.EmailSubject;
        //            //mail.Body = emailSettings.EmailBody;
        //            //mail.IsBodyHtml = true;
        //            mail.subject = emailSettings.EmailSubject;
        //            mail.html = emailSettings.EmailBody;
        //            switch (emailSettings.EmailPriority)
        //            {
        //                case "1":
        //                    //mail.Priority = MailPriority.High;
        //                    mail.important = true;
        //                    break;
        //                case "2":
        //                    //mail.Priority = MailPriority.Normal;
        //                    mail.important = false;
        //                    break;
        //                case "3":
        //                    //mail.Priority = MailPriority.Low;
        //                    mail.important = false;
        //                    break;
        //            }

        //            ////Set Attachments
        //            //emailSettings.EmailAttachments = emailSettings.EmailAttachments.Trim();
        //            //if (emailSettings.EmailAttachments.Length != 0 || emailSettings.EmailAttachments != string.Empty)
        //            //{
        //            //    //Get the list of colon separated attachments into an array
        //            //    string[] arr = emailSettings.EmailAttachments.Split(',');
        //            //    foreach (string str in arr)
        //            //    {
        //            //        //Concatenate the path where attachments are stored, with the actual file name
        //            //        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(emailSettings.EmailAttachmentLocation + "\\" + str);
        //            //        mail.Attachments.Add(attachment);
        //            //    }
        //            //}

        //            //Send the mail
        //            //SmtpClient smtp = new SmtpClient();
        //            MandrillEmailSender emailSender = new MandrillEmailSender(mandrillUserName, mandrillKey);
        //            try
        //            {
        //                //smtp.Host = smtpHost;
        //                //smtp.UseDefaultCredentials = false;
        //                //smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPass);
        //                //smtp.Send(mail);
        //                emailSender.Send(mail);
        //                mailError = string.Empty;
        //                return true;
        //            }
        //            catch (Exception ex)
        //            {
        //                mailError = string.Format("Email failed sending mail through Mandrill api : {0}", ex.Message);
        //                return false;
        //            }
        //        }
        //        mailError = "Email not sent : one or more of the email settings not set.";
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        mailError = string.Format("Email failed : {0}", ex.Message);
        //        return false;
        //    }
        //}

    }
}
