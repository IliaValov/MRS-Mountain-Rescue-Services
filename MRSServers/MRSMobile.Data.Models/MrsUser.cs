using Microsoft.AspNetCore.Identity;
using MRSMobile.Data.Models.Enums;
using System.Collections.Generic;

namespace MRSMobile.Data.Models
{
    public class MrsUser : IdentityUser<string>
    {
        public MrsUser()
        {
            Messages = new List<MrsMessage>();
            Locations = new List<MrsLocation>();
        }

        public TypeUser User { get; set; }

        public bool IsInDanger { get; set; }

        public string DeviceId { get; set; }
        public MrsDevice Device { get; set; }

        public ICollection<MrsMessage> Messages { get; set; }

        public ICollection<MrsLocation> Locations { get; set; }
    }
}
