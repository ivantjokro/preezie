using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Preezie.Presentation.Model
{
    public class UserRequest
    {
        private const string PasswordName = "password";
        private const string Name = "displayname";

        [JsonProperty(PasswordName)]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter and one numeric character.")]
        public string Password { get; set; }

        [JsonProperty(Name)]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Display Name cannot be longer than 50 characters")]
        public string DisplayName { get; set; }
    }
}