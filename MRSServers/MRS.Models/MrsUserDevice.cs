using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models
{
    public class MrsUserDevice
    {
        [Key]
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Type { get; set; }

        public ICollection<MrsUser> MrsUsers { get; set; }
    }
}