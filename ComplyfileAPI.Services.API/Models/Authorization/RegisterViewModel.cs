using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ComplyfileAPI.Services.API.Models.Authorization
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "10015")]
        public string OrganisationName { get; set; }

        [Required(ErrorMessage = "10016")]
        public string OrganisationCountry { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "10004")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "10008")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "10009")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "10010")]
        [RegularExpression("^[a-zA-Z0-9_+\'\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "10011")]
        public string Email { get; set; }

        [Required(ErrorMessage = "10017")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "10018")]
        public string PhonePrefix { get; set; }

        [Required(ErrorMessage = "10019")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "10020")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "10016")]
        public string Country { get; set; }

        [Required(ErrorMessage = "10021")]
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string County { get; set; }
    }
}
