using MRSMobile.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRSMobile.Data.Models
{
    public class MrsSmsAuthantication : BaseModel<int>
    {
        public MrsSmsAuthantication()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Cookie { get; set; }

        [Required]
        public string AuthanticationCode { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public MrsUser User { get; set; }

    }
}
