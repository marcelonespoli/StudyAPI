using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StudyAPI.Business.Interfaces;
using StudyAPI.Business.Validation.Interfaces;
using StudyAPI.Infra.CrossCutting.Models;
using StudyAPI.Infra.CrossCutting.Services.Interfaces;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;
using StudyAPI.Infra.Data.Repository.Repositories.DerivedModels;
using Newtonsoft.Json;

namespace StudyAPI.Business.Business
{
    public class VolunteerManager : IVolunteerManager
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IInviteRepository _inviteRepository;
        private readonly IOrganisationCallbackRepository _organisationCallbackRepository;
        private readonly IEmailFunctions _emailFunctions;
        private readonly IValidation _validation;

        public VolunteerManager(IVolunteerRepository volunteerRepository, IInviteRepository inviteRepository, IOrganisationCallbackRepository organisationCallbackRepository, IEmailFunctions emailFunctions, IValidation validation)
        {
            _volunteerRepository = volunteerRepository;
            _inviteRepository = inviteRepository;
            _organisationCallbackRepository = organisationCallbackRepository;
            _emailFunctions = emailFunctions;
            _validation = validation;
        }

        public Volunteer GetVolunteer(int volunteerId)
        {
            return _volunteerRepository.GetVolunteer(volunteerId);
        }

        public DisplayVolunteer GetVolunteerToDisplay(int volunteerId)
        {
            return _volunteerRepository.GetVolunteerToDisplay(volunteerId);
        }

        public Volunteer GetVolunteerByEmail(string email)
        {
            return _volunteerRepository.GetVolunteerByEmail(email);
        }

        public List<DisplayVolunteer> GetAllVolunteerOfOrganisation(int organisationId)
        {
            return _volunteerRepository.GetAllVolunteerOfOrganisation(organisationId);
        }

        public ResultObject Invite(Volunteer volunteer, string currentUserEmail)
        {
            var validationResult = _validation.ValidateInviteToRegister(volunteer);
            
            if (validationResult.Success)
            {
                var response = new ResultObject();

                var regVolunteer =  RegisterVolunteerInvited(volunteer);
                var regInvite = RegisterInvite(volunteer);
                
                if (regVolunteer != null && regInvite != null)
                {
                    _emailFunctions.SendVolunteerInviteEmail(regVolunteer, regInvite.GUID_VC, volunteer.Organisation_ID, currentUserEmail);

                    response.Success = true;
                    response.Message = "10030";
                    return response;
                }
                
                response.Success = false;
                response.Errors.Add("10014");
                return response;
            }
            
            return validationResult;
        }

        public ResultObject UpdateVolunteer(Volunteer volunteer, int currentUserId)
        {
            var validationResult = _validation.ValidateVolunteerToUpdate(volunteer, currentUserId);

            if (validationResult.Success)
            {
                var response = new ResultObject();
                
                var updateVolunteerSuccess = _volunteerRepository.UpdateVolunteer(volunteer);

                if (updateVolunteerSuccess)
                {
                    response.Success = true;
                    response.Message = "10026";
                    return response;
                }

                response.Success = false;
                response.Errors.Add("10027");
                return response;
            }

            return validationResult;
        }

        public async Task ProcessCallbackAsync(int organisationId, string json)
        {
            var callback = _organisationCallbackRepository.GetOrganisationCallback(organisationId, "UPDATEVOLUNTEER");

            if (callback != null && !string.IsNullOrEmpty(callback.Endpoint_VC))
                await _organisationCallbackRepository.RunCallbackAsync(json, callback.Endpoint_VC);
        }

        #region Private Methods

        private Volunteer RegisterVolunteerInvited(Volunteer volunteer)
        {
            volunteer.VolunteerStatus_ID = 1;
            volunteer.Terminated_BT = false;
            volunteer.ShowWelcome_BT = true;
            volunteer.Uploaded_BT = false;
            volunteer.LastUpdatedBy_ID = volunteer.InvitedBy_ID;
            volunteer.LastUpdated_DT = DateTime.Now;
            volunteer.HideTour_BT = true;
            //v.ManagedBy_ID = vol.ManagedBy_ID;
            //v.Role_ID = vol.Role_ID;

            var success = _volunteerRepository.AddVolunteer(volunteer);
            if (success)
                return volunteer;
            
            return null;
        }

        private Invite RegisterInvite(Volunteer volunteer)
        {
            var invite = new Invite
            {
                GUID_VC = Guid.NewGuid().ToString(),
                Volunteer_ID = volunteer.Volunteer_ID,
                Organisation_ID = volunteer.Organisation_ID,
                IsDeleting_BT = false
            };

            var success = _inviteRepository.AddInvite(invite);
            if (success)
                return invite;

            return null;
        }

        #endregion

    }
}
