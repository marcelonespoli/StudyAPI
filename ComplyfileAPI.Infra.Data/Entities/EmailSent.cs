using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class EmailSent
    {
        [Key]
        public int EmailSent_ID { get; set; }

        [Required]
        [Display(Name = "Email address")]
        public string Email_VC { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string Subject_VC { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message_VC { get; set; }

        public DateTime Date_DT { get; set; }

        public string CC_VC { get; set; }

        public string BCC_VC { get; set; }

        public int Organisation_ID { get; set; }

        public int Template_ID { get; set; }

        public int Entity_ID { get; set; }

        public string EntityType_VC { get; set; }

        public virtual Organisation Organisation { get; set; }

        public int LastUpdatedBy_ID { get; set; }

        public bool CreateActivity { get; set; }

        public int? Status_ID { get; set; }

        public string Token_VC { get; set; }

        public bool ReSent_BT { get; set; }

        public bool Remove_BT { get; set; }

        public virtual EmailSentStatus EmailSentStatus { get; set; }
    }
}
