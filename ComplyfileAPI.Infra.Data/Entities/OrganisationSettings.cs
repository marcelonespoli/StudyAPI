using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class OrganisationSettings
    {
        [Key]
        public int OrganisationSettings_ID { get; set; }

        [Display(Name = "You have selected 'Reference'. How many referees (maximum of 4) must an applicant provide?")]
        public int RefereeCount_IN { get; set; }

        public int Organisation_ID { get; set; }

        [Display(Name = "Signatory")]
        public int? Signatory_ID { get; set; }

        public bool IsSetup_BT { get; set; }

        public bool CriminalCheck_BT { get; set; }

        public Int32 FinancialPlan_ID { get; set; }

        public DateTime NextBillDate_DT { get; set; }

        public DateTime DateCreated_DT { get; set; }

        public bool ExportRequired_BT { get; set; }

        public int BulletClient_ID { get; set; }

        public bool TwoFactorAuthLogin_BT { get; set; }
        
        public DateTime? BulletUpdatedDate_DT { get; set; }

        public virtual Organisation Organisation { get; set; }
        public virtual FinancialPlan FinancialPlan { get; set; }
        public virtual Signatory Signatory { get; set; }
    }
}
