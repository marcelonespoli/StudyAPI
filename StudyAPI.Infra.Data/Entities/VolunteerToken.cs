using System;
using System.Collections.Generic;
using System.Text;

namespace StudyAPI.Infra.Data.Entities
{
    public class VolunteerToken
    {
        public Guid Id { get; set; }
        public int Volunteer_ID { get; set; }
        public string Token_VC { get; set; }
        public bool Active_BT { get; set; }
    }
}
