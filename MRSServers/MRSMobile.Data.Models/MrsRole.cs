using Microsoft.AspNetCore.Identity;
using System;

namespace MRSMobile.Data.Models
{
    public class MrsRole : IdentityRole
    {
        public MrsRole()
            : this(null)
        {
        }

        public MrsRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
