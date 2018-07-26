using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ComplyfileAPI.Business.Interfaces;
using ComplyfileAPI.Business.Validation.Interfaces;
using ComplyfileAPI.Infra.CrossCutting.Helpers;
using ComplyfileAPI.Infra.CrossCutting.Models;
using ComplyfileAPI.Infra.CrossCutting.Security;
using ComplyfileAPI.Infra.Data.Repository.Repositories.DerivedModels;
using ComplyfileAPI.Services.API.CustomContent.ExampleValues;
using ComplyfileAPI.Services.API.Models.Authorization;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;

namespace ComplyfileAPI.Services.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IVolunteerManager _volunteerManager;
        private readonly IOrganisationManager _organisationManager;
        private readonly IValidation _validation;
        private readonly ITokenManager _tokenManager;

        public AuthController(IVolunteerManager volunteerManager, IOrganisationManager organisationManager, IValidation validation, ITokenManager tokenManager)
        {
            _volunteerManager = volunteerManager;
            _organisationManager = organisationManager;
            _validation = validation;
            _tokenManager = tokenManager;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <remarks>
        /// Login the API to have access the organisations
        /// </remarks>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ReturnToken))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(ReturnTokenExamples))]
        [AllowAnonymous]
        [HttpPost]
        [Route("login")] 
        public HttpResponseMessage Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).ToList();

                return Messages.ReturnBadRequestWithLabelErrors(errors);
            }

            try
            {
                if (!_validation.IsVolunteerLoginDataValid(model.Email, model.Password))
                {
                    return Messages.ReturnUnauthorized();
                }

                var volunteer = _volunteerManager.GetVolunteerByEmail(model.Email);

                var generateTokenModel = new GenerateTokenModel
                {
                    VolunterId = volunteer.Volunteer_ID,
                    Email = volunteer.Email_VC,
                    OrganisationId = null,
                    Audience = ConfigurationManager.AppSettings["Audience"],
                    Issuer = ConfigurationManager.AppSettings["Issuer"],
                    Key = ConfigurationManager.AppSettings["Key"]
                };

                var jwtSecurityToken = _tokenManager.GenerateToken(generateTokenModel);

                return Messages.ReturnOk(new
                {
                    token = jwtSecurityToken
                });
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Error while creating token: {ex}");
                return Messages.ReturnInternalServerError();
            }
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <remarks>
        /// Leave the API
        /// </remarks>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ReturnMessage))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(ReturnMessageExamples))]
        [HttpPost]
        [Route("logout")]
        public HttpResponseMessage Logout()
        {
            var token = TokenBase.ReadToken(HttpContext.Current);

            if (_validation.IsTokenValid(token))
            {
                _tokenManager.DeleteAllVolunteerTokens(token.VolunteerId);
                return Messages.ReturnOk(new { message = "10029" });
            }

            return Messages.ReturnUnauthorized(); 
        }

        /// <summary>
        /// List organisations
        /// </summary>
        /// <remarks>
        /// Get all organisations related of the logged user
        /// </remarks>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DisplaySelectOrganisation))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(DisplaySelectOrganisationExamples))]
        [HttpGet]
        [Route("organisation")]
        public HttpResponseMessage Organisation()
        {
            var token = TokenBase.ReadToken(HttpContext.Current);

            if (_validation.IsTokenValid(token))
            {
                var orgList = _organisationManager.GetOrganisationListForCurrentUser(token.Email);

                if (orgList.Any())
                    return Messages.ReturnOk(orgList);

                return Messages.ReturnBadRequestWithLabelErrors("10028");
            }

            return Messages.ReturnUnauthorized();
        }

        /// <summary>
        /// Set default organisation
        /// </summary>
        /// <param name="id">Organisation id</param>
        /// <remarks>
        /// Set the organisation that you want to have access
        /// </remarks>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ReturnToken))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(ReturnTokenExamples))]
        [HttpGet]
        [Route("setdefaultorganisation/{id:int}")]
        public HttpResponseMessage SetDefaultOrganisation(int id)
        {
            var token = TokenBase.ReadToken(HttpContext.Current);

            if (_validation.IsTokenValid(token))
            {
                if (!_validation.VolunteerHasAccessToThisOrg(id, token.Email))
                    return Messages.ReturnUnauthorized();

                var generateTokenModel = new GenerateTokenModel
                {
                    VolunterId = token.VolunteerId,
                    Email = token.Email,
                    OrganisationId = id,
                    Audience = ConfigurationManager.AppSettings["Audience"],
                    Issuer = ConfigurationManager.AppSettings["Issuer"],
                    Key = ConfigurationManager.AppSettings["Key"]
                };

                var jwtSecurityToken = _tokenManager.GenerateToken(generateTokenModel);

                _tokenManager.MakeTokenInvalid(token.TokenId);

                return Messages.ReturnOk(new
                {
                    token = jwtSecurityToken
                });
            }

            return Messages.ReturnUnauthorized();
        }

    }
}
