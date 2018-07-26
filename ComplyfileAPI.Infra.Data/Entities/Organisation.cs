using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class Organisation
    {
        [Key]
        [Display(Name = "Organisation ID")]
        public int Organisation_ID { get; set; }

        [Required(ErrorMessage = "Organisation name is required")]
        [Display(Name = "Name")]
        public string Name_VC { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public int Country_ID { get; set; }

        [Display(Name = "Address")]
        public string Address1_VC { get; set; }

        public string Address2_VC { get; set; }

        public string Address3_VC { get; set; }

        public string Address4_VC { get; set; }

        [Display(Name = "Postcode")]
        public string Postcode_VC { get; set; }

        [Display(Name = "Website")]
        public string Website_VC { get; set; }

        [Display(Name = "Logo")]
        public string Logo_VC { get; set; }

        [Display(Name = "Suspended?")]
        public bool Suspended_BT { get; set; }

        public int LastUpdatedBy_ID { get; set; }

        public DateTime LastUpdated_DT { get; set; }

        public DateTime TrialExpires_DT { get; set; }

        public bool CloseAccount_BT { get; set; }

        [Display(Name = "Hide Billing")]
        public bool HideBilling_BT { get; set; }

        public int PaymentMethod_ID { get; set; }

        [Display(Name = "Currency")]
        public int Currency_ID { get; set; }

        public virtual List<Volunteer> Volunteers { get; set; }

        //public virtual Currency Currency { get; set; }

        [Display(Name = "VAT Number")]
        public string TaxNumber_VC { get; set; }

        public bool IsApproved_BT { get; set; }

        public Nullable<bool> SuspendBilling_BT { get; set; }
    }
}
