using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.Data.Entities;

namespace ComplyfileAPI.Infra.Data.Repository.Interfaces
{
    public interface IFinancialPlanRepository : IRepository<FinancialPlan>
    {
        FinancialPlan GetFinancialPlan(int financialPlanId);
        int? GetMaxVolunteersOfFinancialPlan(int financialPlanId);
    }
}
