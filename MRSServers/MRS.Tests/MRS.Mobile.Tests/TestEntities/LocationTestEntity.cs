using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRS.Tests.MRS.Mobile.Tests.TestEntities
{
    public class LocationTestEntity : IMapFrom<MrsMobileLocation>
    {
        public double Latitude { get; set; }


        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }
        public UserTestEntity User { get; set; }

        public long? MessageId { get; set; }
        public MessageTestEntity Message { get; set; }
    }
}
