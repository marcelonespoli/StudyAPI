using System.Collections.Generic;

namespace StudyAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models.SenderData
{
    public class MandrillEmailMessage
    {
        public string html { get; set; }
        public string text { get; set; }
        public string subject { get; set; }
        public string from_email { get; set; }
        public string from_name { get; set; }
        public List<MandrillRecipient> to { get; set; }
        public bool important { get; set; }
        public bool track_opens { get; set; }
        public bool track_clicks { get; set; }
        public string bcc_address { get; set; }
        public List<MandrillAttachment> attachments { get; set; }
        public List<MandrillImage> images { get; set; }

        public MandrillEmailMessage()
        {
            to = new List<MandrillRecipient>();
            attachments = new List<MandrillAttachment>();
            images = new List<MandrillImage>();
        }
    }
}
