using System.Linq;
using StudyAPI.Business.Validation.Interfaces;
using StudyAPI.Business.Validation.Specifications;
using StudyAPI.Infra.CrossCutting.Models;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Business.Validation
{
    public class Validation : IValidation
    {
        private readonly VolunteerSpecifications _volunteerSpecifications;
        private readonly OrganisationSpecifications _organisationSpecifications;
        private readonly TokenSpecification _tokenSpecification;

        public Validation(VolunteerSpecifications volunteerSpecifications, OrganisationSpecifications organisationSpecifications, TokenSpecification tokenSpecification)
        {
            _volunteerSpecifications = volunteerSpecifications;
            _organisationSpecifications = organisationSpecifications;
            _tokenSpecification = tokenSpecification;
        }

        public bool VolunteerHasAccessToThisOrg(int organisationId, string email)
        {
            return _volunteerSpecifications.VolunteerHasAccessToThisOrg(organisationId, email);
        }

        public bool VolunteerExist(string email)
        {
            return _volunteerSpecifications.VolunteerExist(email);
        }

        public bool IsVolunteerLoginDataValid(string email, string password)
        {
            return _volunteerSpecifications.IsEmailAndPasswordValid(email, password) &&
                   !_volunteerSpecifications.IsVolunteerTerminated(email);
        }

        public bool IsTokenValid(TokenModel token)
        {
            var volunteerToken = new VolunteerToken
            {
                Id = token.TokenId,
                Volunteer_ID = token.VolunteerId,
                Token_VC = token.Token
            };

            return _tokenSpecification.IsTokenActive(volunteerToken) && 
                   token.IsFormatValid;
        }

        public bool IsTokenAndOrganisationValid(TokenModel token)
        {
            return IsTokenValid(token) && 
                   _organisationSpecifications.OrganisationExist(token.OrganisationId);
        }

        public bool VolunteerAlreadyExistToOrganisation(Volunteer volunteer)
        {
            return _volunteerSpecifications.VolunteerAlreadyExistToOrganisation(volunteer.Email_VC, volunteer.Organisation_ID);
        }

        public bool IsOrganisationTrial(int organisationId)
        {
            return _organisationSpecifications.IsOrganisationTrial(organisationId);
        }

        public bool IsOrganisationAtVolunteerLimit(int organisationId)
        {
            return _organisationSpecifications.IsOrgAtVolunteerLimit(organisationId);
        }
        
        public ResultObject ValidateInviteToRegister(Volunteer volunteer)
        {
            var response = new ResultObject();

            if (VolunteerAlreadyExistToOrganisation(volunteer))
            {
                response.Success = false;
                response.Errors.Add("10012");
                return response;
            }

            if (IsOrganisationTrial(volunteer.Organisation_ID) || !IsOrganisationAtVolunteerLimit(volunteer.Organisation_ID))
            {
                response.Success = true;
                return response;
            }
            
            response.Success = false;
            response.Errors.Add("10013");
            
            return response;
        }

        public ResultObject ValidateVolunteerToUpdate(Volunteer vol, int currentUserId)
        {
            var response = new ResultObject();

            if (!_volunteerSpecifications.VolunteerExist(vol.Volunteer_ID))
            {
                response.Success = false;
                response.Errors.Add("10028");
                return response;
            }

            if (!_volunteerSpecifications.VolunteerHasBeenEditedMoreThanTheAllocatedTimeInTheSpaceOf24Hours(vol.Volunteer_ID))
            {
                if (!_volunteerSpecifications.IsVolunteerOrgAdmin(currentUserId, vol.Organisation_ID)||
                    !_volunteerSpecifications.IsEntityValid(vol.Volunteer_ID, vol.Organisation_ID))
                {
                    response.Success = false;
                    response.Errors.Add("10025");
                    return response;
                }

                response.Success = true;
                return response;
            }

            response.Success = false;
            response.Errors.Add("10024");
            return response;
        }

    }
}
