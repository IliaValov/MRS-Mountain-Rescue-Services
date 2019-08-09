using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mrs.Spa.LocalData.Models
{
    public class MrsLDLocation
    {
        public int Id { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        [Required]
        [ForeignKey("Message")]
        public int MessageId { get; set; }
        public MrsLDMessage Message { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public MrsLDUser User { get; set; }
    }
}
