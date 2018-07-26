using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class OrganisationSettingsRepository : GenericRepository<OrganisationSettings>, IOrganisationSettingsRepository
    {
        public OrganisationSettingsRepository(ComplyfileApiContext context) 
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
