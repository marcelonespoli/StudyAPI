using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyAPI.Infra.Data.Repository.Repositories.DerivedModels;
using Swashbuckle.Examples;

namespace StudyAPI.Services.API.CustomContent.ExampleValues
{
    public class DisplaySelectOrganisationExamples : IExamplesProvider
    {
        public object GetExamples()
        {
            return new List<DisplaySelectOrganisation>
            {
                new DisplaySelectOrganisation
                {
                    OrganisationId = 99,
                    Name = "Test organisation 1"
                },
                new DisplaySelectOrganisation
                {
                    OrganisationId = 99,
                    Name = "Test organisation 2"
                }
            };
        }
    }
}