using AutoMapper;
using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using MRS.Mobile.Data.Models.enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MRS.Tests.MRS.Mobile.Tests.TestEntities
{
    [ExcludeFromCodeCoverage]
    public class UserTestEntity: IMapFrom<MrsMobileUser>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        public bool IsInDanger { get; set; }

        public TypeUser UserType { get; set; }

        public DateTime CreatedOn { get; set; }

        public long DeviceId { get; set; }

        public ICollection<MessageTestEntity> Messages { get; set; }

        public ICollection<LocationTestEntity> Locations { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<MrsMobileMessage, UserTestEntity>();

            configuration.CreateMap<MrsMobileLocation, UserTestEntity>();

            configuration.CreateMap<MrsMobileUser, UserTestEntity>()
                .ForMember(x => x.DeviceId, opt => opt.MapFrom(x => x.DeviceId))
                .ForMember(x => x.Locations, opt => opt.MapFrom(x => x.Locations))
                .ForMember(x => x.Messages, opt => opt.MapFrom(x => x.Messages));
        }
    }
}
