using MRSMobile.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRSMobile.Data.Models
{
    public class MrsMessage : BaseDeletableModel<int>
    {
        public MrsMessage()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Condition { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public MrsUser User { get; set; }

        [Required]
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public MrsLocation Location { get; set; }
    }
}
