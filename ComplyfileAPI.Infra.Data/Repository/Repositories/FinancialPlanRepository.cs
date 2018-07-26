using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class FinancialPlanRepository : GenericRepository<FinancialPlan>, IFinancialPlanRepository
    {
        public FinancialPlanRepository(ComplyfileApiContext context) 
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
