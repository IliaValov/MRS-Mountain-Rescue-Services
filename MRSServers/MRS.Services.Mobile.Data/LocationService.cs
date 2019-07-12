using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task AddLocation<T>(T location)
        {
            var newLocation = Mapper.Map<MrsMobileLocation>(location);

            await context.MrsLocations.AddAsync(newLocation);

            await context.SaveChangesAsync();

        }

        public Task<IQueryable<TModel>> All<TModel>() =>
            Task.Run(() => context.MrsLocations.AsQueryable().ProjectTo<TModel>());


        public Task<IQueryable<TModel>> GetByDay<TModel>(DateTime date) =>
            Task.Run(() =>
            context
            .MrsLocations
            .Where(d => d.CreatedOn.Day == date.Day)
            .AsQueryable()
            .ProjectTo<TModel>());
    }
}
