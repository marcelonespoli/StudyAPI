using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ComplyfileAPI.Infra.Data.Entities;

namespace ComplyfileAPI.Services.API.Models.Volunteer
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
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        //[RegularExpression("([\\w-+]+'(?:\\.[\\w-+]+)*@(?:[\\w-]+\\.)+[a-zA-Z]{2,7})", ErrorMessage = "Email is not valid")]
        [RegularExpression("^[a-zA-Z0-9_+\'\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "10011")]
        public string Email { get; set; }
        
        //public int? ManagedBy_ID { get; set; }
        //public int? Role_ID { get; set; }
    }
}
