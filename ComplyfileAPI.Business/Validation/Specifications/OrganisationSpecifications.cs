using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Business.Validation.Specifications
{
    public class OrganisationSpecifications 
    {
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IOrganisationSettingsRepository _organisationSettingsRepository;
        private readonly IOrganisationFinancialPlanRepository _organisationFinancialPlanRepository;
        private readonly IFinancialPlanRepository _financialPlanRepository;
        private readonly IVolunteerRepository _volunteerRepository;

        public OrganisationSpecifications(IOrganisationRepository organisationRepository, IOrganisationSettingsRepository organisationSettingsRepository, IOrganisationFinancialPlanRepository organisationFinancialPlanRepository, IFinancialPlanRepository financialPlanRepository, IVolunteerRepository volunteerRepository)
        {
            _organisationRepository = organisationRepository;
            _organisationSettingsRepository = organisationSettingsRepository;
            _organisationFinancialPlanRepository = organisationFinancialPlanRepository;
            _financialPlanRepository = financialPlanRepository;
            _volunteerRepository = volunteerRepository;
        }

        public bool OrganisationExist(int organisatinId)
        {
            return _organisationRepository.FindBy(o => o.Organisation_ID == organisatinId).Any();
        }

        public bool IsOrganisationTrial(int organisationId)
        {
            var organisationTrialExpiresDate = _organisationRepository.GetTrialExpiresDate(organisationId);
            var orgSettingsFinancialPlanId = _organisationSettingsRepository.GetFinancialPlanId(organisationId);
            var organisationFinancialPlan = _organisationFinancialPlanRepository.GetOrganisationFinancialPlan(organisationId);

            if (organisationTrialExpiresDate > DateTime.Now && 
                orgSettingsFinancialPlanId == 0 && 
                organisationFinancialPlan == null)
            {
                return true;
            }

            return false;
        }

        public bool IsOrgAtVolunteerLimit(int organisationId)
        {
            if (organisationId > 0)
            {
                var totalOfVolunteers = _volunteerRepository.GetTotalNumberOfVolunteers(organisationId);
                var maxVolunteersOfOrganisationFinancialPlan = _organisationFinancialPlanRepository.GetMaxVolunteersOfOrganisationFinancialPlan(organisationId);

                if (maxVolunteersOfOrganisationFinancialPlan != null)
                {
                    if (totalOfVolunteers >= maxVolunteersOfOrganisationFinancialPlan)
                        return true;
                }
                else
                {
                    var orgSettingsFinancialPlanId = _organisationSettingsRepository.GetFinancialPlanId(organisationId);

                    if (orgSettingsFinancialPlanId != null)
                    {
                        var maxVolunteersOfFinancialPlan = _financialPlanRepository.GetMaxVolunteersOfFinancialPlan((int)orgSettingsFinancialPlanId);

                        if (totalOfVolunteers >= maxVolunteersOfFinancialPlan)
                            return true;
                    }
                }
            }

            return false;
        }

    }
}
