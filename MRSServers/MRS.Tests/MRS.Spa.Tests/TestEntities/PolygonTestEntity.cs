using MRS.Common.Mapping;
using MRS.Spa.Data.Models;
using MRS.Spa.Data.Models.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRS.Tests.MRS.Spa.Tests.TestEntities
{
    public class PolygonTestEntity : IMapFrom<MrsSpaPolygon>, IMapTo<MrsSpaPolygon>
    {
        public string Name { get; set; }

        public TypePolygon PolygonType { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
