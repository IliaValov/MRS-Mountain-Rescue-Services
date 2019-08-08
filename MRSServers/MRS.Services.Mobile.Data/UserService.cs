using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MRS.Common.Mapping;
using MRS.Mobile.Data;
using MRS.Mobile.Data.Models;
using MRS.Services.Mobile.Data.Contracts;
using MRS.Spa.Data.Models;

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

        public async Task<IQueryable<TModel>> GetAllUsersWithLocationsWithDateAsync<TModel>(DateTime date) => await Task.Run(() =>
           this.context
            .Users
            .Include(x => x.Locations)
            .Select(x => new MrsMobileUser()
            {
                AccessFailedCount = x.AccessFailedCount,
                ConcurrencyStamp = x.ConcurrencyStamp,
                CreatedOn = x.CreatedOn,
                DeletedOn = x.DeletedOn,
                Device = x.Device,
                DeviceId = x.DeviceId,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                IsInDanger = x.IsInDanger,

                Locations = x.Locations.Where(l =>
                l.CreatedOn.Year == date.Year && 
                l.CreatedOn.Month == date.Month &&
                l.CreatedOn.Day == date.Day).ToList(),

                LockoutEnabled = x.LockoutEnabled,
                LockoutEnd = x.LockoutEnd,
                Messages = x.Messages,
                ModifiedOn = x.ModifiedOn,
                NormalizedEmail = x.NormalizedEmail,
                NormalizedUserName = x.NormalizedUserName,
                PasswordHash = x.PasswordHash,
                PhoneNumber = x.PhoneNumber,
                PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                SecurityStamp = x.SecurityStamp,
                TwoFactorEnabled = x.TwoFactorEnabled,
                UserName = x.UserName
            })
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
