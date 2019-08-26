using MRS.Common.Mapping;
using MRS.Spa.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MRS.Models.MRSSpaModels.BindingModels.MissionLog
{
    public class MissionLogCreateBindingModel : IMapTo<MrsSpaMissionLog>
    {
        [Required]
        public string MissionName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsMissionSuccess { get; set; }

        [Required]
        public string Text { get; set; }

        public string UserId { get; set; }
    }
}
