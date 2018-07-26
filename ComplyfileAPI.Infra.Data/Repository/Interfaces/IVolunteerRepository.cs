﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Repositories.DerivedModels;

namespace ComplyfileAPI.Infra.Data.Repository.Interfaces
{
    public interface IVolunteerRepository : IRepository<Volunteer>
    {
        DisplayVolunteer GetVolunteerToDisplay(int volunteerId);
        Volunteer GetVolunteer(int volunteerId);
        Volunteer GetVolunteer(string email, int organisationId);
        Volunteer GetVolunteer(int volunteerId, int organisationId);
        Volunteer GetVolunteerByEmail(string email);
        Volunteer GetOrgAdmin(int organisationId);
        Volunteer GetOrgAdmin(int volunteerId, int organisationId);
        int GetTotalNumberOfVolunteers(int organisationId);

        bool AddVolunteer(Volunteer volunteer);
        bool UpdateVolunteer(Volunteer volunteer);
        List<DisplayVolunteer> GetAllVolunteerOfOrganisation(int organisationId);

    }
}
