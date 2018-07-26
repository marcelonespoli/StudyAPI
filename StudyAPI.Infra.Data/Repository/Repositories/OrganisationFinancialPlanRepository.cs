using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyAPI.Infra.Data.Context;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Infra.Data.Repository.Repositories
{
    public class OrganisationFinancialPlanRepository : GenericRepository<OrganisationFinancialPlan>, IOrganisationFinancialPlanRepository
    {
        public OrganisationFinancialPlanRepository(StudyAPIContext context) 
            : base(context)
        {
        }

        public OrganisationFinancialPlan GetOrganisationFinancialPlan(int organisationId)
        {
            return FindBy(o => o.Organisation_ID == organisationId).FirstOrDefault();
        }

        public int? GetMaxVolunteersOfOrganisationFinancialPlan(int organisationId)
        {
            return FindBy(f => f.Organisation_ID == organisationId).FirstOrDefault()?.MaxVolunteers_IN;
        }
    }
}
