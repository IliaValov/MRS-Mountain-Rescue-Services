using AutoMapper;
using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using MRS.Models.MRSMobileModels.ViewModels.Location;
using MRS.Models.MRSMobileModels.ViewModels.Message;
using System.Collections.Generic;
using System.Linq;

namespace MRS.Models.MRSMobileModels.ViewModels.Account
{
    public class UserLastLocationViewModel : IMapFrom<MrsMobileUser>, IHaveCustomMappings
    {
        public string PhoneNumber { get; set; }

        public LocationViewModel Location { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<MrsMobileMessage, MessageViewModel>();

            configuration.CreateMap<MrsMobileUser, UserLastLocationViewModel>()
                .ForMember(x => x.Location, opt => opt.MapFrom(x => x.Locations.OrderByDescending(n => n.CreatedOn).FirstOrDefault()))
                .ForMember(x => x.Messages, opt => opt.MapFrom(x => x.Messages));
              

        }
    }
}
