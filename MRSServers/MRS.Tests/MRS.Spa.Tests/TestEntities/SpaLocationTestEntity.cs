using MRS.Common.Mapping;
using MRS.Spa.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MRS.Tests.MRS.Spa.Tests.TestEntities
{
    [ExcludeFromCodeCoverage]
    public class SpaLocationTestEntity : IMapFrom<MrsSpaLocation>, IMapTo<MrsSpaLocation>
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
