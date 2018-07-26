using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyAPI.Services.API.Models.Authorization;
using Swashbuckle.Examples;

namespace StudyAPI.Services.API.CustomContent.ExampleValues
{
    public class ReturnMessageExamples : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ReturnMessage
            {
                Message = "10000"
            };
        }
    }
}

