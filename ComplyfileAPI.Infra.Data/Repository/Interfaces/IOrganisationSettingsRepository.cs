using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.Data.Entities;

namespace ComplyfileAPI.Infra.Data.Repository.Interfaces
{
    public interface IOrganisationSettingsRepository : IRepository<OrganisationSettings>
    {
        OrganisationSettings GetOrganisationSettings(int organisationId);
        int? GetFinancialPlanId(int organisationId);
    }
}
