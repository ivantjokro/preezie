using System.ComponentModel.DataAnnotations.Schema;

namespace Preezie.Presentation.Model
{
    [Table("User")]
    public class User
    {
        public User(string email, string password, string displayName)
        {
            Email = email;
            Password = password;
            DisplayName = displayName;
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }
    }
}
