using System;
using System.Collections.Generic;
using System.Text;

namespace StudyAPI.Infra.Data.Repository.Repositories.DerivedModels
{
    public class DisplayVolunteer
    {
        public int VolunteerId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhonePrefix { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
    }
}
