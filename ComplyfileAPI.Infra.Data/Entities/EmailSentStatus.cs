using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class EmailSentStatus
    {
        [Key]
        public int Status_ID { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description_VC { get; set; }

        [Required]
        [Display(Name = "Icon")]
        public string CssClass_VC { get; set; }
    }
}
