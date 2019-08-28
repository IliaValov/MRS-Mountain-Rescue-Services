using MRS.Common.Mapping;
using MRS.Spa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MRS.Models.MRSSpaModels.ViewModels.MissionLog
{
    public class MissionLogViewModel : IMapFrom<MrsSpaMissionLog>
    {
        public string MissionName { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsMissionSuccess { get; set; }

        public string Details { get; set; }
    }
}
