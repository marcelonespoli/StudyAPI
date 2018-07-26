using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class Document
    {
        [Key]
        [Display(Name = "Document ID")]
        public int Document_ID { get; set; }

        [Display(Name = "Document type")]
        public int DocumentType_ID { get; set; }

        [Display(Name = "Document name")]
        public string DocumentName_VC { get; set; }

        [Display(Name = "Upload date")]
        public DateTime UploadDate_DT { get; set; }

        [Display(Name = "Expiry date")]
        public DateTime? ExpiryDate_DT { get; set; }

        public string BlobName_VC { get; set; }

        public int Organisation_ID { get; set; }

        public int Volunteer_ID { get; set; }
        
        public DateTime? LastUpdatedDate_DT { get; set; }

        public int LastUpdatedBy_ID { get; set; }

        public bool IsDeleting_BT { get; set; }

        [Display(Name = "Visibility")]
        public int Visibility_IN { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Volunteer Volunteer { get; set; }

        public virtual DocumentType DocumentType { get; set; }
    }
}
