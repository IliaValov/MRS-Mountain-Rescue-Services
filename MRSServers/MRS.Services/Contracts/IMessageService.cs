using MRSMobile.Data.Models;
using System;
using System.Linq;

namespace MRS.Services.Contracts
{
    public interface IMessageService
    {
        void AddMessage(MrsMessage message);

        MrsMessage GetLastMessage();

        IQueryable<TModel> GetByDay<TModel>(DateTime date);

        IQueryable<TModel> All<TModel>();


    }
}
