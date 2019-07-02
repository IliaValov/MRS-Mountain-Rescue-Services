using MRSMobile.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRSMobile.Data.Models
{
    public class MrsDevice : BaseDeletableModel<int>
    {
        public MrsDevice()
        {

            this.CreatedOn = DateTime.UtcNow;

            this.Users = new List<MrsUser>();
        }

        [Required]
        public string Device { get; set; }

        public ICollection<MrsUser> Users { get; set; }
    }
}
