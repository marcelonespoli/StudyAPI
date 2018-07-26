using System.Collections.Generic;

namespace StudyAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models
{
    public class MandrillMessageStatus
    {
        public bool MessageSent { get; set; }
        public string MessageError { get; set; }
        public List<MandrillMessageResult> messageResult { get; set; }

        public MandrillMessageError messageError { get; set; }

        public MandrillMessageStatus()
        {
            messageResult = new List<MandrillMessageResult>();
            messageError = new MandrillMessageError();
        }
    }
}
