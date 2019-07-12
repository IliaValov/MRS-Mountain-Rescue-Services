using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MRS.Mobile.Data;
using MRS.Services.Mobile.Data.Contracts;

namespace MRS.Services.Mobile.Data
{
    public class UserService : IUserService
    {

        private readonly MrsMobileDbContext context;

        public UserService(MrsMobileDbContext context)
        {
            this.context = context;
        }

        public async Task<IQueryable<TModel>> All<TModel>() => await Task.Run(() =>
            context
        .Users
        .AsQueryable()
        .ProjectTo<TModel>());

        public async Task ChangeUserCondition(string userId, bool isInDanger)
        {
            var user = context.Users.SingleOrDefault(x => x.Id == userId);

            user.IsInDanger = isInDanger;

            context.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task<T> GetUserById<T>(string phonenumber) => await Task.Run(() =>
            Mapper.Map<T>(context.Users.SingleOrDefault(x => x.PhoneNumber == phonenumber)));


    }
}
