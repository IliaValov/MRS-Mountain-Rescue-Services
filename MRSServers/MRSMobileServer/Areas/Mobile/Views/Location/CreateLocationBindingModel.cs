using MRS.Common.Mapping;
using MRSMobile.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MRSMobileServer.Areas.Mobile.Views.Location
{
    public class CreateLocationBindingModel : IMapTo<MrsMobileLocation>
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        public CreateMessageBindingModel Message { get; set; }
    }
}
