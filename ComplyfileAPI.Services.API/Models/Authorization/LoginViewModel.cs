using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ComplyfileAPI.Services.API.Models.Authorization
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "10001")]
        [EmailAddress(ErrorMessage = "10006")]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "10002")]
        [StringLength(100, ErrorMessage = "100004")]
        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
