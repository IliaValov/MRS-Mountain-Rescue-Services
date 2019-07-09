using MRS.Common.Mapping;
using MRS.Models.MRSMobileModels.ViewModels.Message;
using MRSMobile.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRS.Models.MRSMobileModels.ViewModels.Location
{
    public class LocationViewModel : IMapFrom<MrsMobileLocation>
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }
    }
}
