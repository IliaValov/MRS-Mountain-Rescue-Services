using MRS.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRSMobile.Data.Models
{
    public class MrsMobileLocation : BaseDeletableModel<int>
    {
        public MrsMobileLocation()
        {
            this.CreatedOn = DateTime.UtcNow;
        }


        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        public string UserId { get; set; }
        public MrsMobileUser User { get; set; }

        [ForeignKey("Message")]
        public string MessageId { get; set; }
        public MrsMobileMessage Message { get; set; }
    }
}
