using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using StudyAPI.Infra.CrossCutting.Models;
using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace StudyAPI.Infra.CrossCutting.Helpers
{
    public class Messages 
    {
        public static HttpResponseMessage ReturnOk(object obj)
        {
            var request = CreateRequestToResponse();
            return request.CreateResponse(HttpStatusCode.OK, obj);
        }

        public static HttpResponseMessage ReturnUnauthorized()
        {
            var request = CreateRequestToResponse();
            return request.CreateResponse(HttpStatusCode.Unauthorized, new { errors = "10003" });
        }

        public static HttpResponseMessage ReturnUnauthorized(object obj)
        {
            var request = CreateRequestToResponse();
            return request.CreateResponse(HttpStatusCode.Unauthorized, obj);
        }

        public static HttpResponseMessage ReturnUnauthorizedWithLabelErrors(object obj)
        {
            var request = CreateRequestToResponse();
            return request.CreateResponse(HttpStatusCode.Unauthorized, new { errors = obj });
        }

        public static HttpResponseMessage ReturnBadRequestWithLabelErrors(object obj)
        {
            var request = CreateRequestToResponse();
            return request.CreateResponse(HttpStatusCode.BadRequest, new { errors = obj });
        }

        public static HttpResponseMessage ReturnInternalServerError()
        {
            var request = CreateRequestToResponse();
            return request.CreateResponse(HttpStatusCode.InternalServerError, new { errors = "10005" });
        }

        private static HttpRequestMessage CreateRequestToResponse()
        {
            var request = new HttpRequestMessage();
            request.Properties.Add(System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            return request;
        }

    }

}
