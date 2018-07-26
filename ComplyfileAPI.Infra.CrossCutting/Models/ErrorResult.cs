using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ComplyfileAPI.Infra.CrossCutting.Models
{
    public class ErrorResult
    {
        public ErrorResult()
        {
            Errors = new List<string>();
        }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }
}
