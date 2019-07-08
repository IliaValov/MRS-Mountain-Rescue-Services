using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MRSWeb.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MRSWeb.Data
{
    public class MrsWebUserStore : UserStore<
        MrsWebUser,
        MrsWebRole,
        MrsWebDbContext,
        string,
        IdentityUserClaim<string>,
        IdentityUserRole<string>,
        IdentityUserLogin<string>,
        IdentityUserToken<string>,
        IdentityRoleClaim<string>>
    {
        public MrsWebUserStore(MrsWebDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        protected override IdentityUserRole<string> CreateUserRole(MrsWebUser user, MrsWebRole role)
        {
            return new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id };
        }

        protected override IdentityUserClaim<string> CreateUserClaim(MrsWebUser user, Claim claim)
        {
            var identityUserClaim = new IdentityUserClaim<string> { UserId = user.Id };
            identityUserClaim.InitializeFromClaim(claim);
            return identityUserClaim;
        }

        protected override IdentityUserLogin<string> CreateUserLogin(MrsWebUser user, UserLoginInfo login) =>
            new IdentityUserLogin<string>
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName,
            };

        protected override IdentityUserToken<string> CreateUserToken(
            MrsWebUser user,
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

