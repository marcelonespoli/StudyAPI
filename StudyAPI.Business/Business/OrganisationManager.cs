using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Business.Interfaces;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;
using StudyAPI.Infra.Data.Repository.Repositories.DerivedModels;

namespace StudyAPI.Business.Business
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
