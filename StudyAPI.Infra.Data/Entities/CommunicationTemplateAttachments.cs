using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudyAPI.Infra.Data.Entities
{
    public class CommunicationTemplateAttachments
    {
        [Key]
        public int TemplateAttachment_ID { get; set; }
        public int Template_ID { get; set; }
        public int Organisation_ID { get; set; }
        public int DocumentType_ID { get; set; }
    }
}
