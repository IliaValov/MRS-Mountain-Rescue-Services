using AutoMapper;
using MRS.Common.Mapping;
using MRS.Services.Spa.Data.Contracts;
using MRS.Spa.Data;
using MRS.Spa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data
{
    public class LocationService : ILocationService
    {
        private readonly MrsSpaDbContext context;

        public LocationService(MrsSpaDbContext context)
        {
            this.context = context;
        }

        public async Task AddLocationAsync<TModel>(TModel model)
        {
            var newLocation = Mapper.Map<MrsSpaLocation>(model);

            await this.context.Locations.AddAsync(newLocation);

            await this.context.SaveChangesAsync();
        }

        public async Task<IQueryable<TModel>> GetLocationsByPolygonIdAsync<TModel>(long polygonId)
        {
            return await Task.Run(() =>
          this.context
          .Locations
          .Where(x => x.PolygonId == polygonId)
          .AsQueryable()
          .To<TModel>());
        }
    }
}
