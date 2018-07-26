using System;
using System.Collections.Generic;
using System.Text;

namespace StudyAPI.Infra.CrossCutting.Models
{
    public class TokenModel
    {
        public Guid TokenId { get; set; }
        public DateTime CreateDate { get; set; }
        public int VolunteerId { get; set; }
        public int OrganisationId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsFormatValid { get; set; }
    }
}
