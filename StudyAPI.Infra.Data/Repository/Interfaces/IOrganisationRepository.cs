using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Repositories.DerivedModels;

namespace StudyAPI.Infra.Data.Repository.Interfaces
{
    public interface IOrganisationRepository : IRepository<Organisation>
    {
        Organisation GetOrganisation(int organisationId);
        DateTime? GetTrialExpiresDate(int organisationId);
        List<DisplaySelectOrganisation> GetAllOrganisationByVolunteerEmail(string email);
    }
}
