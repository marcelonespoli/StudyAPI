using System;
using System.Collections.Generic;
using System.Text;

namespace ComplyfileAPI.Infra.CrossCutting.Models
{
    public class GenerateTokenModel
    {
        public int VolunterId { get; set; }
        public string Email { get; set; }
        public int? OrganisationId { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }

    }
}
