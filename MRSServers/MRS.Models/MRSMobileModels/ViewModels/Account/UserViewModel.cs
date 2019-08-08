using AutoMapper;
using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using MRS.Models.MRSMobileModels.ViewModels.Location;
using MRS.Models.MRSMobileModels.ViewModels.Message;
using System.Collections.Generic;

namespace MRS.Models.MRSMobileModels.ViewModels.Account
{
    public class UserViewModel : IMapFrom<MrsMobileUser>, IHaveCustomMappings
    {
        public string PhoneNumber { get; set; }

        public ICollection<LocationViewModel> Locations { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }


        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<MrsMobileMessage, MessageViewModel>();

            configuration.CreateMap<MrsMobileLocation, LocationViewModel>();

            configuration.CreateMap<MrsMobileUser, UserViewModel>()
                .ForMember(x => x.Locations, opt => opt.MapFrom(x => x.Locations))
                .ForMember(x => x.Messages, opt => opt.MapFrom(x => x.Messages));


        }
    }
}
