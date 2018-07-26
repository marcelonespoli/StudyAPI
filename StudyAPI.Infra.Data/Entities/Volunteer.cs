using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyAPI.Infra.Data.Entities
{
    public class Volunteer
    {
        [Key]
        [Display(Name = "Volunteer ID")]
        public int Volunteer_ID { get; set; }

        public string FirstName_VC { get; set; }

        public string Surname_VC { get; set; }

        [NotMapped]
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

        public DateTime DateOfBirth_DT { get; set; }

        public string Mobile_VC { get; set; }

        public string PhonePrefix_VC { get; set; }

        public string Email_VC { get; set; }

        public string Address1_VC { get; set; }

        public string Address2_VC { get; set; }

        public string Address3_VC { get; set; }

        public string Address4_VC { get; set; }

        public string Postcode_VC { get; set; }

        public int Organisation_ID { get; set; }

        public string Password_VC { get; set; }

        public bool IsAuthenticated_BT { get; set; }

        public bool IsAnAdministrator_BT { get; set; }

        public bool IsApplicantFormComplete_BT { get; set; }

        public string ReferredBy_VC { get; set; }

        public string PinCode_VC { get; set; }

        public string ConvictionDetails_VC { get; set; }

        public bool Suspended_BT { get; set; }

        public bool ReceiveUpdates_BT { get; set; }

        public int Country_ID { get; set; }

        public int VolunteerStatus_ID { get; set; }

        public string Gender_VC { get; set; }

        public string Comments_VC { get; set; }

        public string Clickatell_VC { get; set; }

        public int? LastUpdatedBy_ID { get; set; }

        public string Photo_VC { get; set; }

        public DateTime? LastUpdated_DT { get; set; }

        public bool Terminated_BT { get; set; }

        public bool ShowWelcome_BT { get; set; }

        public int InvitedBy_ID { get; set; }

        public DateTime? FormComplete_DT { get; set; }

        public bool Uploaded_BT { get; set; }

        public bool HideTour_BT { get; set; }

        public int? ManagedBy_ID { get; set; }
        public int? Role_ID { get; set; }
        public DateTime? StartDate_DT { get; set; }

        public virtual Organisation Organisation { get; set; }
        public virtual VolunteerStatus VolunteerStatus { get; set; }
        public virtual Country Country { get; set; }
    }
}
