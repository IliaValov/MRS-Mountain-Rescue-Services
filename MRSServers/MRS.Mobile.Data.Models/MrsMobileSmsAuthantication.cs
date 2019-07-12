using MRS.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRS.Mobile.Data.Models
{
    public class MrsMobileSmsAuthantication : BaseModel<long>
    {
        public MrsMobileSmsAuthantication()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Token { get; set; }

        [Required]
        public string AuthanticationCode { get; set; }

        [Required]
        public bool IsUsed { get; set; }

        [Required]
        public DateTime ExpiredAt { get; set; } = DateTime.UtcNow.AddMinutes(15);

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public MrsMobileUser User { get; set; }

    }
}
