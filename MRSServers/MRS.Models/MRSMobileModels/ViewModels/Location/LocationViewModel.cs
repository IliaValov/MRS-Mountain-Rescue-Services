using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using MRS.Models.MRSMobileModels.ViewModels.Message;
using System.Collections.Generic;

namespace MRS.Models.MRSMobileModels.ViewModels.Location
{
    public class LocationViewModel : IMapFrom<MrsMobileLocation>
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public MessageViewModel Message { get; set; }
    }
}
