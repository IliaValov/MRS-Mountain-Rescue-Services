using MRS.Common.Mapping;
using MRS.Spa.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MRS.Tests.MRS.Spa.Tests.TestEntities
{
    [ExcludeFromCodeCoverage]
    public class MissionLogTestEntity : IMapFrom<MrsSpaMissionLog>, IMapTo<MrsSpaMissionLog>
    {
        public string MissionName { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsMissionSuccess { get; set; }

        public string Details { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
