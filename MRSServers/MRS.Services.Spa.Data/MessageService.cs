using AutoMapper;
using Mrs.Spa.LocalData;
using Mrs.Spa.LocalData.Models;
using MRS.Services.Spa.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data
{
    public class MessageService : IMessageService
    {
        private readonly MrsSpaLDContext mrsSpaLDContext;

        public MessageService(MrsSpaLDContext mrsSpaLDContext)
        {
            this.mrsSpaLDContext = mrsSpaLDContext;
        }

        public async Task<bool> AddMessage<TModel>(TModel model)
        {
            try
            {
                await this.mrsSpaLDContext.MrsLDMessages.AddAsync(Mapper.Map<MrsLDMessage>(model));

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

