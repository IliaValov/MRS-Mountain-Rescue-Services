using MRSMobile.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRS.Services.MrsMobileServices.Contracts
{
    public interface ILocationService
    {
        Task AddLocation(MrsMobileLocation location);

        Task<IQueryable<TModel>> GetByDay<TModel>(DateTime date);

        Task<IQueryable<TModel>> All<TModel>();
    }
}
