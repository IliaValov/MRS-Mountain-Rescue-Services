using System.ComponentModel.DataAnnotations;

namespace MRSMobileServer.ViewModels.Account
{
    public class UserLoginBindingModel
    {
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
