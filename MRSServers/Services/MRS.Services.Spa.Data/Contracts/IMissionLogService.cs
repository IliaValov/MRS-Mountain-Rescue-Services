using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data.Contracts
{
    public interface IMissionLogService
    {
        Task AddMissionLogAsync<TModel>(TModel model);

        Task<IQueryable<TModel>> GetAllMissionLogsByUserIdAsync<TModel>(string userId);
    }
}
