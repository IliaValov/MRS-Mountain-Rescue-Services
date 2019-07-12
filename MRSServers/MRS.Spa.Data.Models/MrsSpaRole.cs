using Microsoft.AspNetCore.Identity;
using MRS.Data.Common.Models;
using System;

namespace MRS.Spa.Data.Models
{
    public class MrsSpaRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public MrsSpaRole()
            : this(null)
        {
        }

        public MrsSpaRole(string name)
            : base(name)
        {
            Id = Guid.NewGuid().ToString();

            CreatedOn = DateTime.UtcNow;
        }

        //Audit info
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
