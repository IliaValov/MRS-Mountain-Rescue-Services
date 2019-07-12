using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task<IQueryable<TModel>> All<TModel>() => await Task.Run(() =>
            context
        .Users
        .AsQueryable()
        .ProjectTo<TModel>());

        public async Task ChangeUserCondition(string phonenumber, bool isInDanger)
        {
            var user = context.Users.SingleOrDefault(x => x.PhoneNumber == phonenumber);

            user.IsInDanger = isInDanger;

            context.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task<T> GetUserById<T>(string phonenumber) => await Task.Run(() => 
            Mapper.Map<T>(context.Users.SingleOrDefault(x => x.PhoneNumber == phonenumber)));


    }
}
