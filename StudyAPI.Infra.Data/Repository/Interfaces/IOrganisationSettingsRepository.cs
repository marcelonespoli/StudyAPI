using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Infra.Data.Repository.Interfaces
{
    public interface IOrganisationSettingsRepository : IRepository<OrganisationSettings>
    {
        OrganisationSettings GetOrganisationSettings(int organisationId);
        int? GetFinancialPlanId(int organisationId);
    }
}
