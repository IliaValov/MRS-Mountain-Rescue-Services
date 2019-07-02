using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using MRS.Services.Contracts;
using MRSMobile.Data;
using MRSMobile.Data.Models;

namespace MRS.Services
{
    public class LocationService : ILocationService
    {
        private readonly MrsMobileDbContext context;

        public LocationService(MrsMobileDbContext context)
        {
            this.context = context;
        }

        public async Task AddLocation(MrsLocation location)
        {
            await this.context.MrsLocations.AddAsync(location);

            await this.context.SaveChangesAsync();

        }

        public Task<IQueryable<TModel>> All<TModel>() =>
            Task.Run(() => this.context.MrsLocations.AsQueryable().ProjectTo<TModel>());


        public Task<IQueryable<TModel>> GetByDay<TModel>(DateTime date) =>
            Task.Run(() =>
            this.context
            .MrsLocations
            .Where(d => d.CreatedOn.Day == date.Day)
            .AsQueryable()
            .ProjectTo<TModel>());
    }
}
