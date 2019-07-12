using System.Linq;
using System.Threading.Tasks;

namespace MRS.Services.MrsMobileServices.Contracts
{
    public interface IUserService
    {
        Task<T> GetUserById<T>(string phonenumber);

        Task ChangeUserCondition(string userId, bool isInDanger);

        Task<IQueryable<TModel>> All<TModel>();
    }
}
