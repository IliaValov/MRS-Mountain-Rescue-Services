using MRS.Common.Mapping;
using MRS.Models.MRSMobileModels.BindingModels.Location;
using MRS.Models.MRSMobileModels.BindingModels.Message;
using MRS.Models.MRSMobileModels.RecourceModels.User;
using System.Collections.Generic;

namespace MRS.Models.MRSMobileModels.BindingModels.Account
{
    public class UserAddBindingModel : IMapTo<UserResource>
    {
        public string PhoneNumber { get; set; }

        public ICollection<LocationAddBindingModel> Locations { get; set; }

        public ICollection<MessageAddBindingModel> Messages { get; set; }
    }
}
