using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRS.Services.Mobile.Data.Contracts
{
    public interface IUserService
    {
        Task<T> GetUserByIdAsync<T>(string phonenumber);

        Task ChangeUserConditionAsync(string userId, bool isInDanger);

        Task<IQueryable<TModel>> GetAllUsersWithLocationsWithDateAsync<TModel>(DateTime date);

        Task<IQueryable<TModel>> GetAllAsync<TModel>();
    }
}
