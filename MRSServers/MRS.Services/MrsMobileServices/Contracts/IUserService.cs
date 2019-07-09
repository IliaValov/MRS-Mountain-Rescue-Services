using MRS.Models.MRSMobileModels.ViewModels.Account;
using MRSMobile.Data.Models;
using System.Linq;

namespace MRS.Services.MrsMobileServices.Contracts
{
    public interface IUserService
    {

        MrsMobileUser GetUserById(string id);

        void ChangeUserCondition(string Id, bool isInDanger);

        IQueryable<UserViewModel> All<TModel>();
    }
}
