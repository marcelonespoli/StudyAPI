using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class DocumentType
    {
        [Key]
        [Display(Name = "Document Type ID")]
        public int DocumentType_ID { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Document type name")]
        public string DocumentTypeName_VC { get; set; }

        public string Icon_VC { get; set; }

        public int Organisation_ID { get; set; }

        [Display(Name = "Country")]
        public int Country_ID { get; set; }

        public virtual Country Country { get; set; }
    }
}
