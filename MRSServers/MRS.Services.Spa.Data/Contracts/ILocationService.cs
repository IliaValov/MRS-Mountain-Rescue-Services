using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data.Contracts
{
    public interface ILocationService
    {
        Task<bool> AddLocation<TModel>(TModel model);
    }
}
