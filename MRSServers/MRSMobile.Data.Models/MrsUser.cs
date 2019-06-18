using Microsoft.AspNetCore.Identity;
using MRSMobile.Data.Models.Enums;
using System.Collections.Generic;

namespace MRSMobile.Data.Models
{
    public class MrsUser : IdentityUser<string>
    {
        public TypeUser User { get; set; }

        public bool Condition { get; set; }

        public string DeviceId { get; set; }
        public MrsDevice Device { get; set; }

        public ICollection<MrsLocation> Locations { get; set; }
    }
}
