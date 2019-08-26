using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class PolygonService : IPolygonService
    {
        private readonly MrsSpaDbContext context;

        public PolygonService(MrsSpaDbContext context)
        {
            this.context = context;
        }

        public async Task<long> AddPolygonAsync<TModel>(TModel model)
        {
            var newPolygon = Mapper.Map<MrsSpaPolygon>(model);

            var polygon = await this.context.Polygons.AddAsync(newPolygon);

            await this.context.SaveChangesAsync();

            return polygon.Entity.Id;
        }

        public async Task<IQueryable<TModel>> GetPolygonsByUserIdAsync<TModel>(string userId)
        {
            return await Task.Run(() =>
           this.context
           .Polygons
           .Include(x => x.Locations)
           .Where(x => x.UserId == userId)
           .AsQueryable()
           .To<TModel>());
        }
    }
}
