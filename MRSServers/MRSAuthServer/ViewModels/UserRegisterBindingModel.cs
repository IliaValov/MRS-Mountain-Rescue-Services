using System.ComponentModel.DataAnnotations;

namespace MRSAuthServer.Web.ViewModels
{
    public class UserRegisterBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string PasswordConfirmation { get; set; }
    }
}
