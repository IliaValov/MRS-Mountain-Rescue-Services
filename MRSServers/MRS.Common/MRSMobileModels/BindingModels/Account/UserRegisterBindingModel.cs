using System.ComponentModel.DataAnnotations;

namespace MRS.Models.MRSMobileModels.BindingModels.Account
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
