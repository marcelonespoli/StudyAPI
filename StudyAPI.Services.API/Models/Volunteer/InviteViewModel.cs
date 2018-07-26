using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Services.API.Models.Volunteer
{
    public class InviteViewModel
    {
        [Required(ErrorMessage = "10007")]
        public int OrganisationId { get; set; }

        [Required(ErrorMessage = "10008")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "10009")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "10010")]

        [RegularExpression("^[a-zA-Z0-9_+\'\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "10011")]
        public string Email { get; set; }
    }
}
