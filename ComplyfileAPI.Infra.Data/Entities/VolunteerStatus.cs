using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class VolunteerStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VolunteerStatus_ID { get; set; }

        [Display(Name = "Description")]
        public string Description_VC { get; set; }

        public int Color_ID { get; set; }

        public virtual Color Color { get; set; }
    }
}
