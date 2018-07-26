using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using StudyAPI.Infra.CrossCutting.Models;
using Newtonsoft.Json;

namespace StudyAPI.Infra.CrossCutting.Filters
{
    public class ValidateFormAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                var errors = new ErrorResult
                {
                    Errors = filterContext.Controller.ViewData.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage).ToList()
                };

                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, JsonConvert.SerializeObject(errors)); 
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
