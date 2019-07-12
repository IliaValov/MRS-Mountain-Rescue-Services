using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MRS.Mobile.Data;
using MRS.Mobile.Data.Models;
using MRS.Services.Mobile.Data.Contracts;

namespace MRS.Services.Mobile.Data
{
    public class MessageService : IMessageService
    {
        private readonly MrsMobileDbContext context;

        public MessageService(MrsMobileDbContext context)
        {
            this.context = context;
        }

        public async Task AddMessage<T>(T message)
        {
            var newMessage = Mapper.Map<MrsMobileMessage>(message);
            await context.MrsMessages.AddAsync(newMessage);
            await context.SaveChangesAsync();
        }

        public async Task<IQueryable<TModel>> All<TModel>() => await Task.Run(() => context
        .MrsMessages
        .AsQueryable()
        .ProjectTo<TModel>());

        public async Task<IQueryable<TModel>> GetByDay<TModel>(DateTime date) =>
            await Task.Run(() => context
            .MrsMessages
            .Where(d => d.CreatedOn.Day == date.Day)
            .AsQueryable()
            .ProjectTo<TModel>());


        public async Task<T> GetLastMessage<T>() => await Task.Run(() =>
            context
        .MrsMessages
        .ProjectTo<T>()
        .LastOrDefault());
    }
}
