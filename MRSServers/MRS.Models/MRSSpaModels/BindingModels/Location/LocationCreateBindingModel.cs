using MRS.Common.Mapping;
using MRS.Spa.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models.MRSSpaModels.BindingModels.Location
{
    public class LocationCreateBindingModel : IMapTo<MrsSpaLocation>
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        public long? PolygonId { get; set; }
    }
}
