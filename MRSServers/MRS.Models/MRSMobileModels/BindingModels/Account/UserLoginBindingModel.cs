using System.ComponentModel.DataAnnotations;

namespace MRS.Models.MRSMobileModels.BindingModels.Account
{
    public class UserLoginBindingModel
    {
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
