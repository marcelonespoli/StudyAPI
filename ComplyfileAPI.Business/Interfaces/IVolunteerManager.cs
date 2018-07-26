using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ComplyfileAPI.Infra.CrossCutting.Models;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Repositories.DerivedModels;

namespace ComplyfileAPI.Business.Interfaces
{
    public interface IVolunteerManager
    {
        DisplayVolunteer GetVolunteerToDisplay(int volunteerId);
        Volunteer GetVolunteer(int volunteerId);
        Volunteer GetVolunteerByEmail(string email);
        List<DisplayVolunteer> GetAllVolunteerOfOrganisation(int organisationId);
        ResultObject Invite(Volunteer volunteer, string currentUserEmail);
        ResultObject UpdateVolunteer(Volunteer volunteer, int currentUserId);
        Task ProcessCallbackAsync(int organisationId, string json);
    }
}
