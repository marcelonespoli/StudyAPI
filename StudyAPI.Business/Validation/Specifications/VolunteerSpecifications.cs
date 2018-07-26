using StudyAPI.Infra.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Business.Validation.Specifications
{
    public class VolunteerSpecifications 
    {
        private readonly IOrganisationRepository _organisationRepository;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUpdatedEntityLogRepository _updatedEntityLogRepository;
        private readonly IRefereeRepository _refereeRepository;

        public VolunteerSpecifications(IOrganisationRepository organisationRepository, IVolunteerRepository volunteerRepository, IUpdatedEntityLogRepository updatedEntityLogRepository, IRefereeRepository refereeRepository)
        {
            _organisationRepository = organisationRepository;
            _volunteerRepository = volunteerRepository;
            _updatedEntityLogRepository = updatedEntityLogRepository;
            _refereeRepository = refereeRepository;
        }

        public bool VolunteerHasAccessToThisOrg(int organisationId, string email)
        {
            var selectedOrganisations = _organisationRepository.GetAllOrganisationByVolunteerEmail(email);
            return selectedOrganisations.Any(o => o.OrganisationId == organisationId);
        }

        public bool VolunteerExist(string email)
        {
            var volunteer = _volunteerRepository.GetVolunteerByEmail(email);
            return volunteer != null;
        }

        public bool VolunteerExist(int evolunteerId)
        {
            var volunteer = _volunteerRepository.GetVolunteer(evolunteerId);
            return volunteer != null;
        }


        public bool IsEmailAndPasswordValid(string email, string password)
        {
            var volunteer = _volunteerRepository.GetVolunteerByEmail(email);

            if (volunteer == null)
                return false;

            return volunteer.Password_VC == password;
        }

        public bool IsVolunteerTerminated(string email)
        {
            var volunteer = _volunteerRepository.GetVolunteerByEmail(email);

            if (volunteer == null || volunteer.Terminated_BT)
                return true;

            return false;
        }

        public bool VolunteerAlreadyExistToOrganisation(string email, int organisationId)
        {
            return _volunteerRepository.FindBy(v => v.Email_VC == email && v.Organisation_ID == organisationId).Any();
        }

        public bool VerifyDateRange(DateTime start, DateTime end, DateTime value)
        {
            return start <= value && value <= end;
        }

        public bool VolunteerHasBeenEditedMoreThanTheAllocatedTimeInTheSpaceOf24Hours(int volunteerId)
        {
            var start = DateTime.Now.AddHours(-24);
            var end = DateTime.Now;

            var logs = _updatedEntityLogRepository.GetAllUpdatedEntityLogOfVolunteer(volunteerId);

            var count = 1;
            foreach (var i in logs)
            {
                if (VerifyDateRange(start, end, i.UpdatedDate_DT))
                {
                    if (count == 5)
                        return true;
                    count++;
                }
            }

            return false;
        }

        public bool IsVolunteerOrgAdmin(int volunteerId, int organisationId)
        {
            var volunteer = _volunteerRepository.GetOrgAdmin(volunteerId, organisationId);
            return volunteer != null;
        }

        public bool IsEntityValid(int entityId, int organisationId)
        {
            Volunteer volunteerByReferee = null;
            var volunteer = _volunteerRepository.GetVolunteer(entityId, organisationId);

            // if volunteer is null must also check if the entity Id is not a referee
            // if the entityId is a referee we must also check to make sure the referee belongs to the organisation 
            // so tempVol is related to tempreferee and OrgID is used to check against tempVol
            if (volunteer == null)
            {
                var referee = _refereeRepository.GetReferee(entityId);

                if (referee != null)
                {
                    volunteerByReferee = _volunteerRepository.GetVolunteer(referee.Volunteer_ID, organisationId);
                }
            }

            if (volunteer == null && volunteerByReferee == null)
                return false;

            return true;
        }
    }
}
