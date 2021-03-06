﻿using MRS.Common.Mapping;
using MRS.Mobile.Data.Models;
using MRS.Models.MRSMobileModels.BindingModels.Message;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models.MRSMobileModels.BindingModels.Location
{
    public class LocationCreateBindingModel : IMapTo<MrsMobileLocation>
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        public MessageCreateBindingModel Message { get; set; }
    }
}