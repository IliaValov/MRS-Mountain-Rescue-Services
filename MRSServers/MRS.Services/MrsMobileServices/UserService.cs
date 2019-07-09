using System.Linq;
using AutoMapper.QueryableExtensions;
using MRS.Models.MRSMobileModels.ViewModels.Account;
using MRS.Services.MrsMobileServices.Contracts;
using MRSMobile.Data;
using MRSMobile.Data.Models;

namespace MRS.Services.MrsMobileServices
{
    public class UserService : IUserService
    {

        private readonly MrsMobileDbContext context;

        public UserService(MrsMobileDbContext context)
        {
            this.context = context;
        }

        public IQueryable<UserViewModel> All<TModel>() => context.Users.AsQueryable().ProjectTo<UserViewModel>();

        public void ChangeUserCondition(string id, bool isInDanger)
        {
            var user = context.Users.SingleOrDefault(x => x.Id == id);

            user.IsInDanger = isInDanger;

            context.Update(user);
            context.SaveChanges();
        }

        public MrsMobileUser GetUserById(string id) => context.Users.SingleOrDefault(x => x.Id == id);
    }
}
