
namespace StudyAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models
{
    public class MandrillMessageInfo
    {
        public int ts { get; set; }
        public string _id { get; set; }
        public string state { get; set; }
        public string subject { get; set; }
        public string email { get; set; }
        public int opens { get; set; }
        public int clicks { get; set; }
        public string sender { get; set; }
        public string template { get; set; }
        public string bounce_description { get; set; }

        public MandrillMessageError messageError { get; set; }

        public MandrillMessageInfo()
        {
            messageError = new MandrillMessageError();
        }
    }
}
