using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MRS.Common.Mapping;
using MRS.Mobile.Data;
using MRS.Mobile.Data.Models;
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

        public async Task<IQueryable<TModel>> GetAllAsync<TModel>() => await Task.Run(() =>
            this.context
        .Users
        .Include(x => x.Locations)
        .AsQueryable()
        .To<TModel>());

        public async Task<IQueryable<TModel>> GetAllWithLastLocationAsync<TModel>() => await Task.Run(() =>
           this.context
            .Users
            .Include(x => x.Locations)
            .AsQueryable()
            .To<TModel>());

        public async Task ChangeUserConditionAsync(string userId, bool isInDanger)
        {
            var user = context.Users.SingleOrDefault(x => x.Id == userId);

            user.IsInDanger = isInDanger;

            this.context.Update(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<T> GetUserByIdAsync<T>(string phonenumber) => await Task.Run(() =>
            Mapper.Map<T>(this.context.Users.SingleOrDefault(x => x.PhoneNumber == phonenumber)));


    }
}
