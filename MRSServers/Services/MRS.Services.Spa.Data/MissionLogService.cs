using AutoMapper;
using MRS.Common.Mapping;
using MRS.Services.Spa.Data.Contracts;
using MRS.Spa.Data;
using MRS.Spa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data
{
    public class MissionLogService : IMissionLogService
    {
        private readonly MrsSpaDbContext context;

        public MissionLogService(MrsSpaDbContext context)
        {
            this.context = context;
        }

        public async Task AddMissionLogAsync<TModel>(TModel model)
        {
            var newMissionLog = Mapper.Map<MrsSpaMissionLog>(model);

            await this.context.MissionLogs.AddAsync(newMissionLog);

            await this.context.SaveChangesAsync();
        }

        public async Task<IQueryable<TModel>> GetAllMissionLogsByUserIdAsync<TModel>(string userId)
        {
            return await Task.Run(() =>
          this.context
          .MissionLogs
          .Where(x => x.UserId == userId)
          .AsQueryable()
          .To<TModel>());
        }
    }
}
