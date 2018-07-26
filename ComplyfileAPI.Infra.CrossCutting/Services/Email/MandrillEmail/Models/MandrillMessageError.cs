namespace ComplyfileAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models
{
    public class MandrillMessageError
    {
        public string status { get; set; }
        public int code { get; set; }
        public string name { get; set; }
        public string message { get; set; }
    }
}
