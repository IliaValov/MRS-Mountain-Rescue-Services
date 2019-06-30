using MRSMobile.Data.Models;
using System.Linq;

namespace MRS.Services.Contracts
{
    public interface IUserService
    {

        MrsUser GetUserById(string id);

        void ChangeUserCondition(string Id, bool isInDanger);

        IQueryable<TModel> All<TModel>();
    }
}
