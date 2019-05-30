using MRS.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models
{
    public class MrsRole
    {
        public MrsRole()
        {
            UserRoles = new List<MrsUserRole>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public RoleType Type { get; set; } = RoleType.common;

        public ICollection<MrsUserRole> UserRoles { get; set; }
    }
}