using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Repositories.DerivedModels;

namespace StudyAPI.Business.Interfaces
{
    public interface IOrganisationManager
    {
        List<DisplaySelectOrganisation> GetOrganisationListForCurrentUser(string email);
    }
}
