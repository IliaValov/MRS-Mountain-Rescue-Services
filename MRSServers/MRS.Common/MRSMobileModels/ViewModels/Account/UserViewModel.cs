using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using MRS.Models.MRSMobileModels.ViewModels.Location;
using MRS.Models.MRSMobileModels.ViewModels.Message;
using System.Collections.Generic;

namespace MRS.Models.MRSMobileModels.ViewModels.Account
{
    public class UserViewModel : IMapFrom<MrsMobileUser>
    {
        public string PhoneNumber { get; set; }

        public ICollection<LocationViewModel> Locations { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }

    }
}
