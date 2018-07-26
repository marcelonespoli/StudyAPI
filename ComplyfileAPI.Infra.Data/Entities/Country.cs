using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class Country
    {
        [Key]
        [Display(Name = "Country ID")]
        public int Country_ID { get; set; }

        [Display(Name = "Country")]
        public string CountryName_VC { get; set; }

        [Display(Name = "Phone Prefix")]
        public string PhonePrefix_VC { get; set; }

        [Display(Name = "Display Sequence")]
        public int Sequence_IN { get; set; }

        [Display(Name = "Postcode")]
        public bool Postcode_BT { get; set; }

        [Display(Name = "Direct Debit")]
        public bool DirectDebit_BT { get; set; }

        [Display(Name = "Direct Debit")]
        public bool AllowCreditCard_BT { get; set; }

        public int Currency_ID { get; set; }

        public string CountryCode_VC { get; set; }
        
        [Display(Name = "VAT Percent")]
        [Required(ErrorMessage = "The VAT Percent is required")]
        [Range(0, 999.99, ErrorMessage = "Please enter valid number for VAT Percent!")]
        public Decimal VATPercent_FT { get; set; }

        public bool EUState_BT { get; set; }

        public bool ForceVAT_BT { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual List<Volunteer> Volunteers { get; set; }
    }
}
