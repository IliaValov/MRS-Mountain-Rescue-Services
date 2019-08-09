using AutoMapper;
using MRS.Common.Mapping;
using MRS.Models.MRSMobileModels.BindingModels.Account;
using MRS.Models.MRSMobileModels.BindingModels.Location;
using MRS.Models.MRSMobileModels.BindingModels.Message;
using MRS.Models.MRSMobileModels.RecourceModels.Location;
using MRS.Models.MRSMobileModels.RecourceModels.Message;
using System.Collections.Generic;

namespace MRS.Models.MRSMobileModels.RecourceModels.User
{
    public class UserResource : IMapFrom<UserAddBindingModel>, IHaveCustomMappings
    {
        public string PhoneNumber { get; set; }

        public ICollection<LocationRecource> Locations { get; set; }

        public ICollection<MessageResource> Messages { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<MessageAddBindingModel, MessageResource>();

            configuration.CreateMap<LocationAddBindingModel, LocationRecource>();

            configuration.CreateMap<UserAddBindingModel, UserResource>()
                .ForMember(x => x.Locations, opt => opt.MapFrom(x => x.Locations))
                .ForMember(x => x.Messages, opt => opt.MapFrom(x => x.Messages));
        }
    }
}
