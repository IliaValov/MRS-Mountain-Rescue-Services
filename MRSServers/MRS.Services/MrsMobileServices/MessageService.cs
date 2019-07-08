using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using MRS.Services.MrsMobileServices.Contracts;
using MRSMobile.Data;
using MRSMobile.Data.Models;

namespace MRS.Services.MrsMobileServices
{
    public class MessageService : IMessageService
    {
        private readonly MrsMobileDbContext context;

        public MessageService(MrsMobileDbContext context)
        {
            this.context = context;
        }

        public void AddMessage(MrsMobileMessage message)
        {
            context.MrsMessages.Add(message);
            context.SaveChanges();
        }

        public IQueryable<TModel> All<TModel>() => context.MrsMessages.AsQueryable().ProjectTo<TModel>();

        public IQueryable<TModel> GetByDay<TModel>(DateTime date) => context.MrsMessages
            .Where(d => d.CreatedOn.Day == date.Day)
            .AsQueryable()
            .ProjectTo<TModel>();


        public MrsMobileMessage GetLastMessage() => context.MrsMessages.LastOrDefault();
    }
}
