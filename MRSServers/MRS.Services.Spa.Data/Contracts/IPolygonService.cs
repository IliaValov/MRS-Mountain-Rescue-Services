using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data.Contracts
{
    public interface IPolygonService
    {
        Task<long> AddPolygonAsync<TModel>(TModel model);

        Task<IQueryable<TModel>> GetPolygonsByUserIdAsync<TModel>(string userId);
    }
}
