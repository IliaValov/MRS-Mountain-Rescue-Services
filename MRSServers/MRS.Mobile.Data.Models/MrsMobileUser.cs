using Microsoft.AspNetCore.Identity;
using MRS.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRS.Mobile.Data.Models
{
    public class MrsMobileUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public MrsMobileUser()
        {
            Id = Guid.NewGuid().ToString();
            Messages = new List<MrsMobileMessage>();
            Locations = new List<MrsMobileLocation>();

            CreatedOn = DateTime.UtcNow;
            //this.Roles = new HashSet<IdentityUserRole<string>>();
            //this.Claims = new HashSet<IdentityUserClaim<string>>();
            //this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        public bool IsInDanger { get; set; }

        [Required]
        public long DeviceId { get; set; }
        public MrsMobileDevice Device { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<MrsMobileMessage> Messages { get; set; }

        public ICollection<MrsMobileLocation> Locations { get; set; }

        //public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        //public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        //public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
