using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudyAPI.Infra.Data.Entities
{
    public class FinancialPlan
    {
        [Key]
        public int FinancialPlan_ID { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "The Description is required")]
        public string Description_VC { get; set; }

        [Display(Name = "Payment Terms")]
        [Required(ErrorMessage = "The Payment terms field is required")]
        public string PaymentTerms_VC { get; set; }

        [Required(ErrorMessage = "The Payment amount field is required")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter a valid payment amount. Do not use currency symbols!")]
        public decimal PaymentAmount_FT { get; set; }

        [Required(ErrorMessage = "The Label field is required")]
        public string Label_VC { get; set; }

        [Display(Name = "Comments")]
        [Required(ErrorMessage = "The Comments field is required")]
        public string Comments_VC { get; set; }

        [Display(Name = "Free Trial Days")]
        [Required(ErrorMessage = "The Free trial days field is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid number for Free trial days!")]
        public Int32 FreeTrialDays_IN { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "The Start date is required")]
        public DateTime StartDate_DT { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "The End date is required")]
        public DateTime EndDate_DT { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "The Image field is required")]
        public string Image_VC { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The Name field is required")]
        public string Name_VC { get; set; }

        [Required(ErrorMessage = "The Currency field is required")]
        public int Currency_ID { get; set; }

        [Display(Name = "Maximum Number of Volunteers")]
        [Required(ErrorMessage = "The Maximum Number of Volunteers is required")]
        public int MaxVolunteers_IN { get; set; }
        
        [Display(Name = "Country")]
        [Required(ErrorMessage = "The Country is required")]
        public int Country_ID { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
