using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudyAPI.Infra.Data.Entities
{
    public class Signatory
    {
        [Key]
        [Display(Name = "Signatory ID")]
        public int Signatory_ID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        public string FirstName_VC { get; set; }

        [Required(ErrorMessage = "Surname is required")]
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


        [Required(ErrorMessage = "Mobile phone number is required")]
        [Display(Name = "Mobile")]
        public string Mobile_VC { get; set; }

        public string PhonePrefix_VC { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        [RegularExpression("^[a-zA-Z0-9_+\'\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        [Display(Name = "Email")]
        public string Email_VC { get; set; }

        [Required(ErrorMessage = "Address 1 is required")]
        [Display(Name = "Address")]
        public string Address1_VC { get; set; }

        public string Address2_VC { get; set; }

        public string Address3_VC { get; set; }

        public string Address4_VC { get; set; }

        [Display(Name = "Postcode")]
        public string Postcode_VC { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,15}$", ErrorMessage = "The New password must contain at least 1 number and 1 uppercase letter.")]
        public string Password_VC { get; set; }

        [Display(Name = "Is Authenticated")]
        public bool IsAuthenticated_BT { get; set; }

        [Display(Name = "Pin Code")]
        public string PinCode_VC { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender_VC { get; set; }

        [NotMapped]
        [Display(Name = "Type it again")]
        [Compare("Password_VC", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword_VC { get; set; }

        public int Country_ID { get; set; }

        public virtual Country Country { get; set; }
    }
}
