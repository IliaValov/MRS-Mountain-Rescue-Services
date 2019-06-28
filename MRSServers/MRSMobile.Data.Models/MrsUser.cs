using Microsoft.AspNetCore.Identity;
using MRSMobile.Data.Common.Models;
using System;
using System.Collections.Generic;

namespace MRSMobile.Data.Models
{
    public class MrsUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public MrsUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Messages = new List<MrsMessage>();
            this.Locations = new List<MrsLocation>();

            this.CreatedOn = DateTime.UtcNow;
            //this.Roles = new HashSet<IdentityUserRole<string>>();
            //this.Claims = new HashSet<IdentityUserClaim<string>>();
            //this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        public bool IsInDanger { get; set; }

        public string DeviceId { get; set; }
        public MrsDevice Device { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<MrsMessage> Messages { get; set; }

        public ICollection<MrsLocation> Locations { get; set; }

        //public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        //public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        //public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
