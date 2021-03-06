﻿using StudyAPI.Infra.CrossCutting.Models;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Business.Validation.Interfaces
{
    public interface IValidation
    {
        bool VolunteerHasAccessToThisOrg(int organisationId, string email);
        bool VolunteerExist(string email);
        bool IsVolunteerLoginDataValid(string email, string password);
        bool IsTokenValid(TokenModel token);
        bool IsTokenAndOrganisationValid(TokenModel token);
        bool VolunteerAlreadyExistToOrganisation(Volunteer volunteer);
        bool IsOrganisationTrial(int organisationId);
        bool IsOrganisationAtVolunteerLimit(int organisationId);
        ResultObject ValidateInviteToRegister(Volunteer volunteer);
        ResultObject ValidateVolunteerToUpdate(Volunteer vol, int currentUserId);

    }
}
