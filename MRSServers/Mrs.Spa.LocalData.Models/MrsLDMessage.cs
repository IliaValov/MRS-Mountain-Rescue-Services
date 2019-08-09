using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mrs.Spa.LocalData.Models
{
    public class MrsLDMessage
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public MrsLDUser User { get; set; }

        [Required]
        [ForeignKey("Location")]
        public long LocationId { get; set; }
        public MrsLDLocation Location { get; set; }
    }
}
