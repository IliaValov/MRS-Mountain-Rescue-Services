using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRSMobile.Data.Models
{
    public class MrsLocation
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public MrsUser User { get; set; }

        [ForeignKey("Message")]
        public string MessageId { get; set; }
        public MrsMessage Message { get; set; }
    }
}
