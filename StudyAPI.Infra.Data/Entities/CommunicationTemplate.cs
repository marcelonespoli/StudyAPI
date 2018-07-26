using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudyAPI.Infra.Data.Entities
{
    public class CommunicationTemplate
    {
        [Key]
        public int Template_ID { get; set; }
        [Display(Name = "Name")]
        public string Name_VC { get; set; }
        [Display(Name = "Location")]
        public string Location_VC { get; set; }
        [Display(Name = "Container")]
        public string Container_VC { get; set; }
        [Display(Name = "Entity type")]
        public string EntityType_VC { get; set; }
        [Display(Name = "Subject")]
        public string SubjectLine_VC { get; set; }
        public int Organisation_ID { get; set; }
        public string BlobName_VC { get; set; }
        public bool VolunteerEditable_BT { get; set; }
    }
}
