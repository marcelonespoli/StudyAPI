using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Infra.Data.Repository.Interfaces
{
    public interface IFinancialPlanRepository : IRepository<FinancialPlan>
    {
        FinancialPlan GetFinancialPlan(int financialPlanId);
        int? GetMaxVolunteersOfFinancialPlan(int financialPlanId);
    }
}
