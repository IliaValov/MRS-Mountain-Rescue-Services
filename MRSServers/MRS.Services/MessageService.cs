using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using MRS.Services.Contracts;
using MRSMobile.Data;
using MRSMobile.Data.Models;

namespace MRS.Services
{
    public class MessageService : IMessageService
    {
        private readonly MrsMobileDbContext context;

        public MessageService(MrsMobileDbContext context)
        {
            this.context = context;
        }

        public void AddMessage(MrsMessage message)
        {
            this.context.MrsMessages.Add(message);
            this.context.SaveChanges();
        }

        public IQueryable<TModel> All<TModel>() => this.context.MrsMessages.AsQueryable().ProjectTo<TModel>();

        public IQueryable<TModel> GetByDay<TModel>(DateTime date) => this.context.MrsMessages
            .Where(d => d.CreatedOn.Day == date.Day)
            .AsQueryable()
            .ProjectTo<TModel>();


        public MrsMessage GetLastMessage() => this.context.MrsMessages.LastOrDefault();
    }
}
