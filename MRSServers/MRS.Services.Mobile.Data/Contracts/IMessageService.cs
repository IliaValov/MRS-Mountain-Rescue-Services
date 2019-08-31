using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRS.Services.Mobile.Data.Contracts
{
    public interface IMessageService
    {
        Task AddMessageAsync<T>(T message);

        Task<T> GetLastMessageAsync<T>();

        Task<IQueryable<TModel>> GetAllMessagesByDateAsync<TModel>(DateTime date);

        Task<IQueryable<TModel>> GetAllAsync<TModel>();


    }
}
