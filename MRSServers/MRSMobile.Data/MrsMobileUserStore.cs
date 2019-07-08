using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MRSMobile.Data.Models;
using System.Security.Claims;

namespace MRSMobile.Data
{
    public class MrsMobileUserStore : UserStore<
        MrsMobileUser,
        MrsMobileRole,
        MrsMobileDbContext,
        string,
        IdentityUserClaim<string>,
        IdentityUserRole<string>,
        IdentityUserLogin<string>,
        IdentityUserToken<string>,
        IdentityRoleClaim<string>>
    {
        public MrsMobileUserStore(MrsMobileDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        protected override IdentityUserRole<string> CreateUserRole(MrsMobileUser user, MrsMobileRole role)
        {
            return new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id };
        }

        protected override IdentityUserClaim<string> CreateUserClaim(MrsMobileUser user, Claim claim)
        {
            var identityUserClaim = new IdentityUserClaim<string> { UserId = user.Id };
            identityUserClaim.InitializeFromClaim(claim);
            return identityUserClaim;
        }

        protected override IdentityUserLogin<string> CreateUserLogin(MrsMobileUser user, UserLoginInfo login) =>
            new IdentityUserLogin<string>
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName,
            };

        protected override IdentityUserToken<string> CreateUserToken(
            MrsMobileUser user,
            string loginProvider,
            string name,
            string value)
        {
            var token = new IdentityUserToken<string>
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value,
            };
            return token;
        }
    }
}
