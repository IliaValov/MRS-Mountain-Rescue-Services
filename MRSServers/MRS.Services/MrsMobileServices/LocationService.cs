using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using MRS.Services.MrsMobileServices.Contracts;
using MRSMobile.Data;
using MRSMobile.Data.Models;

namespace MRS.Services.MrsMobileServices
{
    public class LocationService : ILocationService
    {
        private readonly MrsMobileDbContext context;

        public LocationService(MrsMobileDbContext context)
        {
            this.context = context;
        }

        public async Task AddLocation(MrsMobileLocation location)
        {
            await context.MrsLocations.AddAsync(location);

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
