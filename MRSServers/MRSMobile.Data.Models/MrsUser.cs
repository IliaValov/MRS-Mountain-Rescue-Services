using Microsoft.AspNetCore.Identity;
using MRSMobile.Data.Models.Enums;
using System;
using System.Collections.Generic;

namespace MRSMobile.Data.Models
{
    public class MrsUser : IdentityUser<string>
    {
        public MrsUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Messages = new List<MrsMessage>();
            this.Locations = new List<MrsLocation>();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        public TypeUser User { get; set; }

        public bool IsInDanger { get; set; }

        public string DeviceId { get; set; }
        public MrsDevice Device { get; set; }

        public ICollection<MrsMessage> Messages { get; set; }

        public ICollection<MrsLocation> Locations { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
