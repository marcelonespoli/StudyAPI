using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.Data.Entities;

namespace ComplyfileAPI.Infra.Data.Repository.Interfaces
{
    public interface IOrganisationFinancialPlanRepository : IRepository<OrganisationFinancialPlan>
    {
        OrganisationFinancialPlan GetOrganisationFinancialPlan(int organisationId);
        int? GetMaxVolunteersOfOrganisationFinancialPlan(int organisationId);
    }
}
