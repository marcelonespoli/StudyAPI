using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Repositories.DerivedModels;

namespace ComplyfileAPI.Business.Interfaces
{
    public interface IOrganisationManager
    {
        List<DisplaySelectOrganisation> GetOrganisationListForCurrentUser(string email);
    }
}
