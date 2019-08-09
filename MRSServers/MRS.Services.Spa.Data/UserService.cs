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
    public class UserService : IUserService
    {
        private readonly MrsSpaLDContext mrsSpaLDContext;

        public UserService(MrsSpaLDContext mrsSpaLDContext)
        {
            this.mrsSpaLDContext = mrsSpaLDContext;
        }

        public async Task<bool> AddUser<TModel>(TModel model)
        {
            try
            {
                await this.mrsSpaLDContext.MrsLDUsers.AddAsync(Mapper.Map<MrsLDUser>(model));

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<bool> AddUsersWithLocations<TModel>(TModel model)
        {
            throw new NotImplementedException();
        }
    }
}
