using System;
using System.Linq;
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

        public void AddLocation(MrsLocation location)
        {
            this.context.MrsLocations.Add(location);

            this.context.SaveChanges();

           
        }

        public IQueryable<TModel> All<TModel>() => this.context.MrsLocations.AsQueryable().ProjectTo<TModel>();

        public IQueryable<TModel> GetByDay<TModel>(DateTime date) => this.context.MrsLocations.Where(d => d.CreatedOn.Day == date.Day).AsQueryable().ProjectTo<TModel>();
    }
}
