using MRSMobile.Data.Models;
using System;
using System.Linq;

namespace MRS.Services.Contracts
{
    public interface ILocationService
    {
        void AddLocation(MrsLocation location);

        IQueryable<TModel> GetByDay<TModel>(DateTime date);

        IQueryable<TModel> All<TModel>();
    }
}
