using AutoMapper;
using Mrs.Spa.LocalData;
using Mrs.Spa.LocalData.Models;
using MRS.Services.Spa.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data
{
    public class LocationService : ILocationService
    {
        private readonly MrsSpaLDContext mrsSpaLDContext;

        public LocationService(MrsSpaLDContext mrsSpaLDContext)
        {
            this.mrsSpaLDContext = mrsSpaLDContext;
        }

        public async Task<bool> AddLocation<TModel>(TModel model)
        {
            try
            {
                await this.mrsSpaLDContext.MrsLDLocations.AddAsync(Mapper.Map<MrsLDLocation>(model));

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
