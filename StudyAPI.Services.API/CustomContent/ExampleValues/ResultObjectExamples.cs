using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyAPI.Infra.CrossCutting.Models;
using StudyAPI.Infra.Data.Repository.Repositories.DerivedModels;
using Swashbuckle.Examples;

namespace StudyAPI.Services.API.CustomContent.ExampleValues
{
    public class ResultObjectExamples : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ResultObject
            {
                Success = true,
                Message = "10000",
                Errors = new List<string>()
            };
        }
    }
}