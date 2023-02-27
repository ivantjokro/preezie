using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Preezie.Presentation.Model
{
    public class CreateUserRequest : UserRequest
    {
        private const string EmailName = "email";

        [JsonProperty(EmailName)]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}
