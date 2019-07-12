using MRS.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRS.Mobile.Data.Models
{
    public class MrsMobileDevice : BaseDeletableModel<long>
    {
        public MrsMobileDevice()
        {

            CreatedOn = DateTime.UtcNow;

            Users = new List<MrsMobileUser>();
        }

        [Required]
        public string Device { get; set; }

        public ICollection<MrsMobileUser> Users { get; set; }
    }
}
