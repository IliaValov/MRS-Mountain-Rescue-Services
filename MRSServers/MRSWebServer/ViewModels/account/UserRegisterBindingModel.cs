using System.ComponentModel.DataAnnotations;

namespace MRSWebServer.ViewModels.account
{
    public class UserRegisterBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
