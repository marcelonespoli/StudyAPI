using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyAPI.Infra.Data.Context;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;
using StudyAPI.Infra.Data.Repository.Repositories.DerivedModels;

namespace StudyAPI.Infra.Data.Repository.Repositories
{
    public class VolunteerRepository : GenericRepository<Volunteer>, IVolunteerRepository
    {
        public VolunteerRepository(StudyAPIContext context) 
            : base(context)
        {
        }

        public DisplayVolunteer GetVolunteerToDisplay(int volunteerId)
        {
            return (from vol in Context.Volunteer
                    join volStatus in Context.VolunteerStatus on vol.VolunteerStatus_ID equals volStatus.VolunteerStatus_ID into statusGroup from volStatus in statusGroup.DefaultIfEmpty()
                    join country in Context.Country on vol.Country_ID equals country.Country_ID into countryGroup
                    from country in countryGroup.DefaultIfEmpty()
                    where vol.Volunteer_ID == volunteerId
                    select new DisplayVolunteer
                    {
                        VolunteerId = vol.Volunteer_ID,
                        FirstName = vol.FirstName_VC,
                        Surname = vol.Surname_VC,
                        DateOfBirth = vol.DateOfBirth_DT,
                        Gender = vol.Gender_VC,
                        Email = vol.Email_VC,
                        PhonePrefix = vol.PhonePrefix_VC,
                        Mobile = vol.Mobile_VC,
                        Address1 = vol.Address1_VC,
                        Address2 = vol.Address2_VC,
                        Address3 = vol.Address3_VC,
                        Address4 = vol.Address4_VC,
                        Postcode = vol.Postcode_VC,
                        Country = country.CountryName_VC,
                        Status = volStatus.Description_VC
                    })
                .FirstOrDefault();
        }

        public Volunteer GetVolunteer(int volunteerId)
        {
            return FindBy(f => f.Volunteer_ID == volunteerId).FirstOrDefault();
        }

        public Volunteer GetVolunteer(string email, int organisationId)
        {
            return FindBy(f => f.Email_VC == email && f.Organisation_ID == organisationId).FirstOrDefault();
        }

        public Volunteer GetVolunteer(int volunteerId, int organisationId)
        {
            return FindBy(f => f.Volunteer_ID == volunteerId && f.Organisation_ID == organisationId).FirstOrDefault();
        }

        public Volunteer GetVolunteerByEmail(string email)
        {
            return FindBy(f => f.Email_VC == email).FirstOrDefault();
        }

        public int GetTotalNumberOfVolunteers(int organisationId)
        {
            return FindBy(f => f.Organisation_ID == organisationId).Count();
        }

        public Volunteer GetOrgAdmin(int organisationId)
        {
            return FindBy(f => 
                f.Organisation_ID == organisationId && 
                f.IsAnAdministrator_BT &&
                f.IsAuthenticated_BT).FirstOrDefault();
        }

        public Volunteer GetOrgAdmin(int volunteerId, int organisationId)
        {
            return FindBy(f =>
                f.Volunteer_ID == volunteerId &&
                f.Organisation_ID == organisationId &&
                f.IsAnAdministrator_BT).FirstOrDefault();
        }

        public List<DisplayVolunteer> GetAllVolunteerOfOrganisation(int organisationId)
        {
            return (from vol in Context.Volunteer
                    join volStatus in Context.VolunteerStatus on vol.VolunteerStatus_ID equals volStatus.VolunteerStatus_ID into statusGroup from volStatus in statusGroup.DefaultIfEmpty()
                    join country in Context.Country on vol.Country_ID equals country.Country_ID into countryGroup from country in countryGroup.DefaultIfEmpty()
                    where vol.Organisation_ID == organisationId
                    select new DisplayVolunteer
                    {
                        VolunteerId = vol.Volunteer_ID,
                        FirstName = vol.FirstName_VC,
                        Surname = vol.Surname_VC,
                        DateOfBirth = vol.DateOfBirth_DT,
                        Gender = vol.Gender_VC,
                        Email = vol.Email_VC,
                        PhonePrefix = vol.PhonePrefix_VC,
                        Mobile = vol.Mobile_VC,
                        Address1 = vol.Address1_VC,
                        Address2 = vol.Address2_VC,
                        Address3 = vol.Address3_VC,
                        Address4 = vol.Address4_VC,
                        Postcode = vol.Postcode_VC,
                        Country = country.CountryName_VC,
                        Status = volStatus.Description_VC
                    })
                    .OrderBy(o => o.FirstName)
                    .ToList();
        }

        public bool AddVolunteer(Volunteer volunteer)
        {
            try
            {
                Insert(volunteer);
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateVolunteer(Volunteer volunteer)
        {
            try
            {
                Update(volunteer);
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
    }
}
