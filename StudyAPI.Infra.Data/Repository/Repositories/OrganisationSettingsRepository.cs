using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyAPI.Infra.Data.Context;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Infra.Data.Repository.Repositories
{
    public class OrganisationSettingsRepository : GenericRepository<OrganisationSettings>, IOrganisationSettingsRepository
    {
        public OrganisationSettingsRepository(StudyAPIContext context) 
            : base(context)
        {
        }

        public OrganisationSettings GetOrganisationSettings(int organisationId)
        {
            return FindBy(f => f.Organisation_ID == organisationId).FirstOrDefault();
        }

        public int? GetFinancialPlanId(int organisationId)
        {
            return FindBy(o => o.Organisation_ID == organisationId).FirstOrDefault()?.FinancialPlan_ID;
        }
    }
}
