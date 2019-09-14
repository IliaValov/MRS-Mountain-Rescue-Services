using MRS.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MRS.Spa.Data.Models
{
    public class MrsSpaLocation : BaseDeletableModel<long>
    {
        public MrsSpaLocation()
        {
            CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }


        public long PolygonId { get; set; }
        public MrsSpaPolygon Polygon { get; set; }
    }
}
