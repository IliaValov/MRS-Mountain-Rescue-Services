using MRS.Common.Mapping;
using MRS.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRSMobile.Data.Models
{
    public class MrsMobileDevice : BaseDeletableModel<long>
    {
        public MrsMobileDevice()
        {

            this.CreatedOn = DateTime.UtcNow;

            this.Users = new List<MrsMobileUser>();
        }

        [Required]
        public string Device { get; set; }

        public ICollection<MrsMobileUser> Users { get; set; }
    }
}
