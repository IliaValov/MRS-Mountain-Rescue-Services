using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRSMobile.Data.Models
{
    public class MrsDevice
    {
        public MrsDevice()
        {
            Users = new List<MrsUser>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Device { get; set; }

        public ICollection<MrsUser> Users { get; set; }
    }
}
