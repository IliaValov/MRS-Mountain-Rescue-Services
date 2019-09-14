using MRS.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MRS.Spa.Data.Models
{
    public class MrsSpaMissionLog : BaseDeletableModel<long>
    {
        public MrsSpaMissionLog()
        {
            CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string MissionName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsMissionSuccess { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string UserId { get; set; }
        public MrsSpaUser User { get; set; }
    }
}
