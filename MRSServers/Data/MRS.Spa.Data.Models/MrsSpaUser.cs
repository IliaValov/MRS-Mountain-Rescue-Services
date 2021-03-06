﻿using Microsoft.AspNetCore.Identity;
using MRS.Data.Common.Models;
using System;
using System.Collections.Generic;

namespace MRS.Spa.Data.Models
{
    public class MrsSpaUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public MrsSpaUser()
        {
            Id = Guid.NewGuid().ToString();

            CreatedOn = DateTime.UtcNow;

            this.Polygons = new List<MrsSpaPolygon>();

            this.MissionLogs = new List<MrsSpaMissionLog>();
            //this.Roles = new HashSet<IdentityUserRole<string>>();
            //this.Claims = new HashSet<IdentityUserClaim<string>>();
            //this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }

        public ICollection<MrsSpaMissionLog> MissionLogs { get; set; }

        public ICollection<MrsSpaPolygon> Polygons { get; set; }
        //public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        //public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        //public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
