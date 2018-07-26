using System;

namespace ComplyfileAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models.SenderData
{
    public class MandrillSenderData
    {
        public string key { get; set; }
        public MandrillEmailMessage message { get; set; }
        public bool async { get; set; }
        public string ip_pool { get; set; }
        public DateTime send_at { get; set; }

        public MandrillSenderData()
        {
            message = new MandrillEmailMessage();
        }

    }
}
