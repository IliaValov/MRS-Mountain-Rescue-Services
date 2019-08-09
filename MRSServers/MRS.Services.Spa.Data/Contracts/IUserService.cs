using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data.Contracts
{
    public interface IUserService
    {
        Task<bool> AddUser<TModel>(TModel model);

        Task<bool> AddUsersWithLocations<TModel>(TModel model);
    }
}
