using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MRS.Common.Mapping;
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

        public async Task AddMessageAsync<T>(T message)
        {
            var newMessage = Mapper.Map<MrsMobileMessage>(message);
            await this.context.Messages.AddAsync(newMessage);
            await this.context.SaveChangesAsync();
        }

        public async Task<IQueryable<TModel>> GetAllAsync<TModel>() => await Task.Run(() => this.context
        .Messages
        .AsQueryable()
        .To<TModel>());


        public async Task<IQueryable<TModel>> GetAllMessagesByDateAsync<TModel>(DateTime date) =>
            await Task.Run(() => this.context
            .Messages
            .Where(d => d.CreatedOn.Day == date.Day)
            .To<TModel>());


        public async Task<T> GetLastMessageAsync<T>() => await Task.Run(() =>
            context
        .Messages
        .OrderByDescending(x => x.Id)
        .To<T>()
        .FirstOrDefault());
    }
}
