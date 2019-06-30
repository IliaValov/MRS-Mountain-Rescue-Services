using System.ComponentModel.DataAnnotations;

namespace MRSMobileServer.ViewModels.Account
{
    public class UserRegisterBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string PasswordConfirmation { get; set; }
    }
}
