using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Repositories.DerivedModels;

namespace ComplyfileAPI.Infra.Data.Repository.Interfaces
{
    public interface IOrganisationRepository : IRepository<Organisation>
    {
        Organisation GetOrganisation(int organisationId);
        DateTime? GetTrialExpiresDate(int organisationId);
        List<DisplaySelectOrganisation> GetAllOrganisationByVolunteerEmail(string email);
    }
}
