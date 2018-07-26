using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyAPI.Infra.Data.Context;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Infra.Data.Repository.Repositories
{
    public class FinancialPlanRepository : GenericRepository<FinancialPlan>, IFinancialPlanRepository
    {
        public FinancialPlanRepository(StudyAPIContext context) 
            : base(context)
        {
        }

        public FinancialPlan GetFinancialPlan(int financialPlanId)
        {
            return FindBy(f => f.FinancialPlan_ID == financialPlanId).FirstOrDefault();
        }

        public int? GetMaxVolunteersOfFinancialPlan(int financialPlanId)
        {
            return FindBy(f => f.FinancialPlan_ID == financialPlanId).FirstOrDefault()?.MaxVolunteers_IN;
        }
    }
}
