using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MRS.Tests.MRS.Mobile.Tests.TestEntities
{
    [ExcludeFromCodeCoverage]
    public class LocationTestEntity : IMapFrom<MrsMobileLocation>
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
