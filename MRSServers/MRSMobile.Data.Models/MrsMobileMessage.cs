using MRS.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRSMobile.Data.Models
{
    public class MrsMobileMessage : BaseDeletableModel<long>
    {
        public MrsMobileMessage()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Condition { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public MrsMobileUser User { get; set; }

        [Required]
        [ForeignKey("Location")]
        public long LocationId { get; set; }
        public MrsMobileLocation Location { get; set; }
    }
}
