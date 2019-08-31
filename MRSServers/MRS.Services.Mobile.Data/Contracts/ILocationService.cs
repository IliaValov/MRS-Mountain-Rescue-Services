using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRS.Services.Mobile.Data.Contracts
{
    public interface ILocationService
    {
        Task AddLocationAsync<T>(T location);

        Task<IQueryable<TModel>> GetByDateAsync<TModel>(DateTime date);

        Task<IQueryable<TModel>> GetAllAsync<TModel>();
    }
}
