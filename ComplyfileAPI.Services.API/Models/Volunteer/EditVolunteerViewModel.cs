using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplyfileAPI.Services.API.Models.Volunteer
{
    public class EditVolunteerViewModel
    {
        [Required(ErrorMessage = "10022")]
        public int VolunterId { get; set; }

        [Required(ErrorMessage = "10008")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "10023")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "10010")]
        [RegularExpression("^[a-zA-Z0-9_+\'\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "10011")]
        public string Email { get; set; }

        [Required(ErrorMessage = "10017")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "10018")]
        public string PhonePrefix { get; set; }

        [Required(ErrorMessage = "10019")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "10020")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "10016")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "10021")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }

        public bool? AdministratorPrivileges { get; set; }
        public bool? ReceiveEmailUpdates { get; set; }

        public int? ManagedById { get; set; }
        public int? RoleId { get; set; }
        public DateTime? StartDate { get; set; }
    }
}