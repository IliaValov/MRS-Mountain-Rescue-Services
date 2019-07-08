using Microsoft.AspNetCore.Identity;
using MRS.Data.Common.Models;
using System;

namespace MRSMobile.Data.Models
{
    public class MrsMobileRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public MrsMobileRole()
            : this(null)
        {
        }

        public MrsMobileRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();

            this.CreatedOn = DateTime.UtcNow;
        }

        //Audit info
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
