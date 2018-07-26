using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Repositories.DerivedModels;
using Swashbuckle.Examples;
using Swashbuckle.Swagger;

namespace StudyAPI.Services.API.CustomContent.ExampleValues
{
    public class DisplayVolunteerExamples : IExamplesProvider
    {
        public object GetExamples()
        {
            return new DisplayVolunteer
            {
                VolunteerId = 99,
                FirstName = "Test name",
                Surname = "Test surname",
                DateOfBirth = DateTime.Today.AddYears(-20),
                Gender = "Female",
                PhonePrefix = "+353",
                Mobile = "088 1522033",
                Email = "test@email.com",
                Address1 = "Address 1",
                Address2 = "Address 2",
                Address3 = "Address 3",
                Address4 = "Address 4",
                Postcode = "T12 FX47",
                Country = "Ireland",
                Status = "Active"
            };
        }
    }
}