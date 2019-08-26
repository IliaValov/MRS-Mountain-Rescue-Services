﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Services.Spa.Data.Contracts
{
    public interface ILocationService
    {
        Task AddLocation<TModel>(TModel model);

        Task<IQueryable<TModel>> GetLocationsByPolygonId<TModel>(long polygonId);
    }
}
