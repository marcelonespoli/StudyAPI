using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComplyfileAPI.Infra.CrossCutting.Models;
using ComplyfileAPI.Infra.Data.Repository.Repositories.DerivedModels;
using Swashbuckle.Examples;

namespace ComplyfileAPI.Services.API.CustomContent.ExampleValues
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