using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using StudyAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models.SenderData;

namespace StudyAPI.Infra.CrossCutting.Models
{
    public class EmailModel
    {
        public MailMessage MailMessage { get; set; }
        public MandrillEmailMessage MandrillEmailMessage { get; set; }
        public int OrganisationId { get; set; }
        public int EntityId { get; set; }
        public int TemplateId { get; set; }
        public string EntityType { get; set; }
        public string CurrentUserEmail { get; set; }
    }
}
