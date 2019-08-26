using AutoMapper;
using MRS.Common.Mapping;
using MRS.Models.MRSSpaModels.BindingModels.Location;
using MRS.Spa.Data.Models;
using MRS.Spa.Data.Models.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MRS.Models.MRSSpaModels.BindingModels.Polygon
{
    public class PolygonCreateBindingModel : IMapTo<MrsSpaPolygon>, IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PolygonType { get; set; }

        public ICollection<LocationCreateBindingModel> Locations { get; set; }

        public string UserId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PolygonCreateBindingModel, MrsSpaPolygon>()
                .ForMember(x => x.PolygonType, opt => opt.MapFrom(x => Enum.Parse<TypePolygon>(x.PolygonType)));
        }
    }
}
