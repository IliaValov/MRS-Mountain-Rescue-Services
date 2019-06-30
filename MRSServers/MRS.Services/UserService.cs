using System.Linq;
using AutoMapper.QueryableExtensions;
using MRS.Services.Contracts;
using MRSMobile.Data;
using MRSMobile.Data.Models;

namespace MRS.Services
{
    public class UserService : IUserService
    {

        private readonly MrsMobileDbContext context;

        public UserService(MrsMobileDbContext context)
        {
            this.context = context;
        }

        public IQueryable<TModel> All<TModel>() => this.context.Users.AsQueryable().ProjectTo<TModel>();

        public void ChangeUserCondition(string id, bool isInDanger)
        {
            var user = this.context.Users.SingleOrDefault(x => x.Id == id);

            user.IsInDanger = isInDanger;

            this.context.Update(user);
            this.context.SaveChanges();
        }

        public MrsUser GetUserById(string id) => this.context.Users.SingleOrDefault(x => x.Id == id);
    }
}
