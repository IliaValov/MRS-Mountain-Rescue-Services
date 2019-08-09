using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data.Contracts
{
    public interface IMessageService
    {
        Task<bool> AddMessage<TModel>(TModel model);
    }
}
