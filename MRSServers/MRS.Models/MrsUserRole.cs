using System.ComponentModel.DataAnnotations;

namespace MRS.Models
{
    public class MrsUserRole
    {
        [Required]
        public int UserId { get; set; }
        public MrsUser User { get; set; }

        [Required]
        public int RoleId { get; set; }
        public MrsRole Role { get; set; }
    }
}