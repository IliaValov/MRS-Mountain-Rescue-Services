using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRS.Tests.MRS.Mobile.Tests.TestEntities
{
    public class DeviceTestEntity : IMapFrom<MrsMobileDevice>
    {
        public string Device { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<UserTestEntity> Users { get; set; }
    }
}
