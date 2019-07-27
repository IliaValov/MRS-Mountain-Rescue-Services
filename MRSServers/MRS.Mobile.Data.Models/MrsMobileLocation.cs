using MRS.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRS.Mobile.Data.Models
{
    public class MrsMobileLocation : BaseDeletableModel<long>
    {
        public MrsMobileLocation()
        {
            CreatedOn = DateTime.UtcNow;
        }


        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        [Required]
        public string UserId { get; set; }
        public MrsMobileUser User { get; set; }

        [ForeignKey("Message")]
        public long? MessageId { get; set; }
        public MrsMobileMessage Message { get; set; }
    }
}
