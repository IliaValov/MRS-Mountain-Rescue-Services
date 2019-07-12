using MRS.Models.MRSMobileModels.ViewModels.Location;
using MRSMobile.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRS.Services.MrsMobileServices.Contracts
{
    public interface ILocationService
    {
        Task AddLocation<T>(T location);

        Task<IQueryable<TModel>> GetByDay<TModel>(DateTime date);

        Task<IQueryable<TModel>> All<TModel>();
    }
}
