using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class Referee
    {
        [Key]
        [Display(Name = "Referee ID")]
        public int Referee_ID { get; set; }

        [Required(ErrorMessage = "Referee first name is required")]
        [Display(Name = "First name")]
        public string FirstName_VC { get; set; }

        [Required(ErrorMessage = "Referee last name is required")]
        [Display(Name = "Last name")]
        public string Surname_VC { get; set; }

        [NotMapped]
        [Display(Name = "Name")]
        public string FullName_VC
        {
            get
            {
                return FirstName_VC + " " + Surname_VC;
            }
            set
            {

            }
        }

        public string PhonePrefix_VC { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile_VC { get; set; }

        [Required(ErrorMessage = "Referee email address is required")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        [RegularExpression("^[a-zA-Z0-9_+\'\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        [Display(Name = "Email")]
        public string Email_VC { get; set; }

        [Display(Name = "Address")]
        public string Address1_VC { get; set; }

        public string Address2_VC { get; set; }

        public string Address3_VC { get; set; }

        public string Address4_VC { get; set; }

        [Display(Name = "Postcode")]
        public string Postcode_VC { get; set; }

        public int Country_ID { get; set; }

        [Required]
        public int Volunteer_ID { get; set; }

        public bool RefereeFormComplete_BT { get; set; }

        public bool SentEmail_BT { get; set; }

        public DateTime? FormComplete_DT { get; set; }

        public int? LastUpdatedBy_ID { get; set; }

        public virtual Volunteer Volunteer { get; set; }
        public virtual Country Country { get; set; }
    }
}
