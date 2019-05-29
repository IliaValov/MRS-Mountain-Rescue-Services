using System;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models
{
    public class MrsLocation
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int UserId { get; set; }
        public MrsUser User { get; set; }

        public int? ImergencyMessageId { get; set; }
        public MrsImergencyMessage ImergencyMessage { get; set; }
    }
}