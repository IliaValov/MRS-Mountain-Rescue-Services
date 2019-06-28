using System.ComponentModel.DataAnnotations;

namespace MRSMobileServer.ViewModels.Location
{
    public class CreateLocationBindingModel
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }
    }
}
