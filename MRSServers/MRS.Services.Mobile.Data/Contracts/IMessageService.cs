using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRS.Services.Mobile.Data.Contracts
{
    public interface IMessageService
    {
        Task AddMessage<T>(T message);

        Task<T> GetLastMessage<T>();

        Task<IQueryable<TModel>> GetByDay<TModel>(DateTime date);

        Task<IQueryable<TModel>> GetAllAsync<TModel>();


    }
}
