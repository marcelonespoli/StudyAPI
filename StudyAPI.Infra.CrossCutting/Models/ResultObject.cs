using System.Collections.Generic;

namespace StudyAPI.Infra.CrossCutting.Models
{
    public class ResultObject
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public ResultObject()
        {
            Errors = new List<string>();
        }
    }
}
