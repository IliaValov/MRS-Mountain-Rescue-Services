using MRS.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRSMobile.Data.Models
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
        [ForeignKey("User")]
        public string UserId { get; set; }
        public MrsMobileUser User { get; set; }

    }
}
