
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementCollection.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }


        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
