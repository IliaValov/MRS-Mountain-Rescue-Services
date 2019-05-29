using MRS.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models
{
    public class MrsRole
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public RoleType Type { get; set; } = RoleType.Common;

        public ICollection<MrsUserRole> UserRoles { get; set; }
    }
}