using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRS.Data.Models
{
    public class MrsDevice
    {
        public MrsDevice()
        {
            this.Id = System.Guid.NewGuid().ToString();
            this.Users = new HashSet<ApplicationUser>();
        }
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Type { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
