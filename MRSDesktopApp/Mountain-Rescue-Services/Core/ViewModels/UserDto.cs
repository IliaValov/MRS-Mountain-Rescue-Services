using GMap.NET.WindowsForms.Markers;
using System.Collections.Generic;

namespace Mountain_Rescue_Services.Core.ViewModels
{
    public class UserDto
    {
        public string PhoneNumber { get; set; }

        public bool IsSavior { get; set; }

        public bool IsOnline { get; set; }

        public bool IsInDanger { get; set; }

        public GMarkerGoogleType Marker { get => SetMarker(); }

        public List<string> EmergencyMessages { get; set; }

        public List<LocationDto> Locations { get; set; }

        private GMarkerGoogleType SetMarker()
        {
            if (IsSavior)
            {
                return GMarkerGoogleType.blue_small;
            }
            else if (IsInDanger)
            {
                return GMarkerGoogleType.red_small;
            }
            else
            {
                return GMarkerGoogleType.green_small;
            }
        }
    }
}
