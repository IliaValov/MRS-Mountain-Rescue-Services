using AutoMapper;
using MRS.Common.Mapping;
using MRS.Models.MRSSpaModels.ViewModels.Location;
using MRS.Spa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRS.Models.MRSSpaModels.ViewModels.Polygon
{
    public class PolygonViewModel : IMapFrom<MrsSpaPolygon>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string PolygonType { get; set; }

        public ICollection<LocationViewModel> Locations { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<MrsSpaPolygon, PolygonViewModel>()
                .ForMember(x => x.PolygonType, opt => opt.MapFrom(x => x.PolygonType.ToString()));
        }
    }
}
