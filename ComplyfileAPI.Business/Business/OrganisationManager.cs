using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Business.Interfaces;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;
using ComplyfileAPI.Infra.Data.Repository.Repositories.DerivedModels;

namespace ComplyfileAPI.Business.Business
{
    public class OrganisationManager : IOrganisationManager
    {
        private readonly IOrganisationRepository _organisationRepository;

        public OrganisationManager(IOrganisationRepository organisationRepository)
        {
            _organisationRepository = organisationRepository;
        }     

        public List<DisplaySelectOrganisation> GetOrganisationListForCurrentUser(string email)
        {
            return _organisationRepository.GetAllOrganisationByVolunteerEmail(email);
        }
    }
}
