using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MRSMobile.Data.Models;
using System.Security.Claims;

namespace MRSMobile.Data
{
    public class MrsRoleStore : RoleStore<
        MrsRole,
        MrsMobileContext,
        string,
        IdentityUserRole<string>,
        IdentityRoleClaim<string>>
    {
        public MrsRoleStore(MrsMobileContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        protected override IdentityRoleClaim<string> CreateRoleClaim(MrsRole role, Claim claim) =>
            new IdentityRoleClaim<string>
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            };
    }
}
