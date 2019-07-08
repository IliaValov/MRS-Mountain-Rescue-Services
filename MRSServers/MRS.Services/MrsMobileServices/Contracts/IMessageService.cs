using MRSMobile.Data.Models;
using System;
using System.Linq;

namespace MRS.Services.MrsMobileServices.Contracts
{
    public interface IMessageService
    {
        void AddMessage(MrsMobileMessage message);

        MrsMobileMessage GetLastMessage();

        IQueryable<TModel> GetByDay<TModel>(DateTime date);

        IQueryable<TModel> All<TModel>();


    }
}
