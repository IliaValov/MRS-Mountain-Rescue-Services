using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data.Contracts
{
    public interface ILocationService
    {
        Task AddLocationAsync<TModel>(TModel model);

        Task<IQueryable<TModel>> GetLocationsByPolygonIdAsync<TModel>(long polygonId);
    }
}
