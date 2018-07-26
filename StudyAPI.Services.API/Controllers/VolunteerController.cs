using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using StudyAPI.Business.Interfaces;
using StudyAPI.Business.Validation.Interfaces;
using StudyAPI.Infra.CrossCutting.Helpers;
using StudyAPI.Infra.CrossCutting.Models;
using StudyAPI.Infra.CrossCutting.Security;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Repositories.DerivedModels;
using StudyAPI.Services.API.CustomContent.ExampleValues;
using StudyAPI.Services.API.Models.Volunteer;
using Newtonsoft.Json;
using Swashbuckle.Examples;
using Swashbuckle.Swagger;
using Swashbuckle.Swagger.Annotations;

namespace StudyAPI.Services.API.Controllers
{
    [Authorize]
    [RoutePrefix("api")]
    public class VolunteerController : ApiController
    {
        private readonly IVolunteerManager _volunteerManager;
        private readonly IValidation _validation;

        public VolunteerController(IVolunteerManager volunteerManager, IValidation validation)
        {
            _volunteerManager = volunteerManager;
            _validation = validation;
        }

        /// <summary>
        /// Provide all volunteers for the organisation
        /// </summary>
        /// <remarks>
        /// Get a list of volunteers for the organisation setted as default
        /// </remarks>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DisplayVolunteer))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(DisplayVolunteerListExamples))]
        [HttpGet]
        [Route("volunteers")]
        public HttpResponseMessage Get()
        {
            var token = TokenBase.ReadToken(HttpContext.Current);

            if (_validation.IsTokenAndOrganisationValid(token))
            {
                var volunteerList = _volunteerManager.GetAllVolunteerOfOrganisation(token.OrganisationId);

                if (volunteerList.Any())
                    return Messages.ReturnOk(volunteerList);

                return Messages.ReturnBadRequestWithLabelErrors("10028");
            }

            return Messages.ReturnUnauthorized();
        }


        /// <summary>
        /// Provide details for a volunteer
        /// </summary>
        /// <remarks>
        /// Get a specific volunteer
        /// </remarks>
        /// <param name="id">Volunteer id</param>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(DisplayVolunteer) )]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(DisplayVolunteerExamples))]
        [HttpGet]
        [Route("volunteer/{id:int}")]
        public HttpResponseMessage GetVolunteer(int id)
        {
            var token = TokenBase.ReadToken(HttpContext.Current);

            if (_validation.IsTokenAndOrganisationValid(token))
            {
                var volunteer = _volunteerManager.GetVolunteerToDisplay(id);

                if (volunteer != null)
                    return Messages.ReturnOk(volunteer);

                return Messages.ReturnBadRequestWithLabelErrors("10028");
            }

            return Messages.ReturnUnauthorized();
        }

        /// <summary>
        /// Invite a new volunteer
        /// </summary>
        /// <remarks>
        /// Invite a new volunteer for the organisation setted as default
        /// </remarks>
        [SwaggerResponse(HttpStatusCode.OK, null,  Type = typeof(ResultObject))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(ResultObjectExamples))]
        [HttpPost]
        [Route("volunteer/invite")]
        public HttpResponseMessage Post([FromBody] InviteViewModel invite)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).ToList();

                return Messages.ReturnBadRequestWithLabelErrors(errors);
            }

            var token = TokenBase.ReadToken(HttpContext.Current);

            if (_validation.IsTokenAndOrganisationValid(token))
            {
                var volunteer = new Volunteer
                {
                    Organisation_ID = invite.OrganisationId,
                    FirstName_VC = invite.FirstName,
                    Surname_VC = invite.Surname,
                    Email_VC = invite.Email,
                    InvitedBy_ID = token.VolunteerId
                };

                var resultInvite = _volunteerManager.Invite(volunteer, token.Email);

                if (resultInvite.Success)
                    return Messages.ReturnOk(resultInvite);

                return Messages.ReturnUnauthorized(resultInvite);
            }

            return Messages.ReturnUnauthorized();
        }

        /// <summary>
        /// Update a volunteer
        /// </summary>
        /// <remarks>
        /// Update details of specific volunteer
        /// </remarks>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ResultObject))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(ResultObjectExamples))]
        [HttpPut]
        [Route("volunteer")]
        public HttpResponseMessage Put([FromBody] EditVolunteerViewModel editVolunteer)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage).ToList();

                return Messages.ReturnBadRequestWithLabelErrors(errors);
            }
            
            var token = TokenBase.ReadToken(HttpContext.Current);

            if (_validation.IsTokenAndOrganisationValid(token))
            {
                var volunteer = PrepereVolunteerToUpdate(editVolunteer, token.VolunteerId);

                if (volunteer == null)
                {
                    var response = new ResultObject();
                    response.Success = false;
                    response.Errors.Add("10028");
                    return Messages.ReturnUnauthorized(response); 
                }

                var resultUpdate = _volunteerManager.UpdateVolunteer(volunteer, token.VolunteerId);

                if (resultUpdate.Success)
                {
                    _volunteerManager.ProcessCallbackAsync(token.OrganisationId, JsonConvert.SerializeObject(editVolunteer));
                    return Messages.ReturnOk(resultUpdate);
                }

                return Messages.ReturnUnauthorized(resultUpdate);
            }

            return Messages.ReturnUnauthorized();
        }


        #region Private Methods

        private Volunteer PrepereVolunteerToUpdate(EditVolunteerViewModel editVolunteer, int currentUserId)
        {
            try
            {
                var volunteer = _volunteerManager.GetVolunteer(editVolunteer.VolunterId);

                // required fields
                volunteer.FirstName_VC = editVolunteer.FirstName;
                volunteer.Surname_VC = editVolunteer.LastName;
                volunteer.Email_VC = editVolunteer.Email;
                volunteer.Mobile_VC = editVolunteer.Mobile;
                volunteer.PhonePrefix_VC = editVolunteer.PhonePrefix;
                volunteer.DateOfBirth_DT = editVolunteer.DateOfBirth;
                volunteer.Gender_VC = editVolunteer.Gender;
                volunteer.Country_ID = editVolunteer.CountryId;
                volunteer.Address1_VC = editVolunteer.Address1;
                volunteer.Address2_VC = editVolunteer.Address2;
                volunteer.Address3_VC = editVolunteer.Address3;
                volunteer.Address4_VC = editVolunteer.Address4;
                volunteer.LastUpdated_DT = DateTime.Now;
                volunteer.LastUpdatedBy_ID = currentUserId;

                // not required fields
                if (editVolunteer.AdministratorPrivileges != null)
                    volunteer.IsAnAdministrator_BT = (bool)editVolunteer.AdministratorPrivileges;

                if (editVolunteer.ReceiveEmailUpdates != null)
                    volunteer.ReceiveUpdates_BT = (bool)editVolunteer.ReceiveEmailUpdates;

                if (editVolunteer.ManagedById != null)
                    volunteer.ManagedBy_ID = editVolunteer.ManagedById;

                if (editVolunteer.RoleId != null)
                    volunteer.Role_ID = editVolunteer.RoleId;

                if (editVolunteer.StartDate != null)
                    volunteer.StartDate_DT = editVolunteer.StartDate;

                return volunteer;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        #endregion
        
    }
}
