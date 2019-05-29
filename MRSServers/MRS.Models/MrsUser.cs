using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models
{
    public class MrsUser
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public int AccessFailedCount { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTime LockoutEnd { get; set; }

        [Required]
        public bool PhoneNumberConfirmed { get; set; } = false;

       
        [Required]
        public int DeviceId { get; set; }
        public MrsUserDevice Device { get; set; }

        [Required]
        public int AuthanticationTokenId { get; set; }
        public MrsUserAuthanticationToken AuthanticationToken { get; set; }

        public ICollection<MrsUserVerification> UserVerifications { get; set; }
        public ICollection<MrsUserRole> UserRoles { get; set; }
        public ICollection<MrsLocation> Locations { get; set; }
    }
}
