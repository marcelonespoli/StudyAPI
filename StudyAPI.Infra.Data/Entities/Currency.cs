using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudyAPI.Infra.Data.Entities
{
    public class Currency
    {
        [Key]
        public int Currency_ID { get; set; }

        [Display(Name = "Currency name")]
        public string CurrencyName_VC { get; set; }

        [Display(Name = "Currency symbol")]
        public string Symbol_VC { get; set; }

        [Display(Name = "Currency code")]
        public string CurrencyCode_VC { get; set; }
    }
}
