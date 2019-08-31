using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MRS.Common.Mapping;
using MRS.Mobile.Data;
using MRS.Mobile.Data.Models;
using MRS.Services.Mobile.Data.Contracts;

namespace MRS.Services.Mobile.Data
{
    public class LocationService : ILocationService
    {
        private readonly MrsMobileDbContext context;

        public LocationService(MrsMobileDbContext context)
        {
            this.context = context;
        }

        public async Task AddLocationAsync<T>(T location)
        {
            var newLocation = Mapper.Map<MrsMobileLocation>(location);

            await this.context.Locations.AddAsync(newLocation);

            await this.context.SaveChangesAsync();

        }

        public Task<IQueryable<TModel>> GetAllAsync<TModel>() =>
            Task.Run(() => this.context.Locations.AsQueryable().ProjectTo<TModel>());


        public Task<IQueryable<TModel>> GetByDateAsync<TModel>(DateTime date) =>
            Task.Run(() =>
            this.context
            .Locations
            .Where(d => d.CreatedOn.Day == date.Day)
            .AsQueryable()
            .To<TModel>());
    }
}
