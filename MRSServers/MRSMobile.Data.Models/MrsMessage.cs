using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRSMobile.Data.Models
{
    public class MrsMessage
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [ForeignKey("User")]
        public string UserId { get; set; }
        public MrsUser User { get; set; }

        [Required]
        [ForeignKey("Location")]
        public string LocationId { get; set; }
        public MrsLocation Location { get; set; }
    }
}
