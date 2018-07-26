using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyAPI.Infra.Data.Repository.Repositories.DerivedModels;
using Swashbuckle.Examples;

namespace StudyAPI.Services.API.CustomContent.ExampleValues
{
    public class DisplayVolunteerListExamples : IExamplesProvider
    {
        public object GetExamples()
        {
            return new List<DisplayVolunteer>
            {
                new DisplayVolunteer
                {
                    VolunteerId = 55,
                    FirstName = "Test name 1",
                    Surname = "Test surname 1",
                    DateOfBirth = DateTime.Today.AddYears(-20),
                    Gender = "Female",
                    PhonePrefix = "+353",
                    Mobile = "088 1522033",
                    Email = "test1@email.com",
                    Address1 = "Address 1",
                    Address2 = "Address 2",
                    Address3 = "Address 3",
                    Address4 = "Address 4",
                    Postcode = "T12 FX47",
                    Country = "Ireland",
                    Status = "Active"
                },
                new DisplayVolunteer
                {
                    VolunteerId = 56,
                    FirstName = "Test name 2",
                    Surname = "Test surname 2",
                    DateOfBirth = DateTime.Today.AddYears(-30),
                    Gender = "Female",
                    PhonePrefix = "+353",
                    Mobile = "088 5211453",
                    Email = "test2@email.com",
                    Address1 = "Address 1",
                    Address2 = "Address 2",
                    Address3 = "Address 3",
                    Address4 = "Address 4",
                    Postcode = "T08 DV53",
                    Country = "Ireland",
                    Status = "Active"
                }
            };
        }
    }
}
