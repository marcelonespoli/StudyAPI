using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class Invite
    {
        [Key]
        public int Invite_ID { get; set; }

        public string GUID_VC { get; set; }

        public int Volunteer_ID { get; set; }

        public int Organisation_ID { get; set; }

        public int? LastUpdatedBy_ID { get; set; }
        public bool IsDeleting_BT { get; set; }
    }
}
