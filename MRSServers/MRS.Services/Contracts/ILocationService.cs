using MRSMobile.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRS.Services.Contracts
{
    public interface ILocationService
    {
        Task AddLocation(MrsLocation location);

        Task<IQueryable<TModel>> GetByDay<TModel>(DateTime date);

        Task<IQueryable<TModel>> All<TModel>();
    }
}
