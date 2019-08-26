using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data.Contracts
{
    public interface IMissionLogService
    {
        Task AddMissionLog<TModel>(TModel model);

        Task<IQueryable<TModel>> GetAllMissionLogsByUserId<TModel>(string userId);
    }
}
