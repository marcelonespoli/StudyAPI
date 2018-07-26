using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyAPI.Infra.Data.Repository.Repositories.DerivedModels;
using StudyAPI.Services.API.Models.Authorization;
using Swashbuckle.Examples;

namespace StudyAPI.Services.API.CustomContent.ExampleValues
{
    public class ReturnTokenExamples : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ReturnToken
            {          
                Token = "bjKloksSxiOibWIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJhNzOS1kNzcwZTIMS8yNC8yMDE0lkXBseWXVkIjoiY29tcGx5ZmlsZS5jb20ifQ"
            };
        }
    }
}
