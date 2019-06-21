using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRSMobile.Data.Models
{
    public class MrsDevice
    {
        public MrsDevice()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new List<MrsUser>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Device { get; set; }

        public ICollection<MrsUser> Users { get; set; }
    }
}
