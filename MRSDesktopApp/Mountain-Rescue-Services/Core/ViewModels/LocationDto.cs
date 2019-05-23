using System;

namespace Mountain_Rescue_Services.Core.ViewModels
{
    public class LocationDto
   {
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double Altitude { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }
    }
}
