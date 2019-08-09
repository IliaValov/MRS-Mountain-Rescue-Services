using MRS.Common.Mapping;
using MRS.Models.MRSMobileModels.BindingModels.Message;
using MRS.Models.MRSMobileModels.RecourceModels.Location;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models.MRSMobileModels.BindingModels.Location
{
    public class LocationAddBindingModel : IMapTo<LocationRecource>
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        public MessageAddBindingModel Message { get; set; }
    }
}
