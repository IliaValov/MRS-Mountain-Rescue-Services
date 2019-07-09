using MRS.Common.Mapping;
using MRS.Models.MRSMobileModels.BindingModels.Message;
using MRSMobile.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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