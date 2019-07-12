using MRS.Models.MRSMobileModels.BindingModels.Message;
using MRS.Models.MRSMobileModels.ViewModels.Message;
using MRSMobile.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRS.Services.MrsMobileServices.Contracts
{
    public interface IMessageService
    {
        Task AddMessage<T>(T message);

        Task<T> GetLastMessage<T>();

        Task<IQueryable<TModel>> GetByDay<TModel>(DateTime date);

        Task<IQueryable<TModel>> All<TModel>();


    }
}
