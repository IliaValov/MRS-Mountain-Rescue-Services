using MRS.Common.Mapping;
using MRS.Models.MRSMobileModels.BindingModels.Location;
using MRS.Models.MRSMobileModels.RecourceModels.Message;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models.MRSMobileModels.RecourceModels.Location
{
    public class LocationRecource : IMapFrom<LocationAddBindingModel>
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        public MessageResource Message { get; set; }
    }
}
