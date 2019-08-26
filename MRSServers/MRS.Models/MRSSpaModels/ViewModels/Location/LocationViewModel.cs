using MRS.Common.Mapping;
using MRS.Spa.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MRS.Models.MRSSpaModels.ViewModels.Location
{
    public class LocationViewModel : IMapFrom<MrsSpaLocation>
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }
    }
}
