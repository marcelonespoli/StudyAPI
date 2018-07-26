using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Infra.Data.Repository.Interfaces
{
    public interface IOrganisationFinancialPlanRepository : IRepository<OrganisationFinancialPlan>
    {
        OrganisationFinancialPlan GetOrganisationFinancialPlan(int organisationId);
        int? GetMaxVolunteersOfOrganisationFinancialPlan(int organisationId);
    }
}
