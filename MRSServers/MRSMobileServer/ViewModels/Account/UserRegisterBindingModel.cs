using System.ComponentModel.DataAnnotations;

namespace MRSMobileServer.ViewModels.Account
{
    public class UserRegisterBindingModel
    {
        
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Device { get; set; }
    }
}
