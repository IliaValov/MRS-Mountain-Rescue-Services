using MRS.Data.Common.Models;
using MRS.Spa.Data.Models.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MRS.Spa.Data.Models
{
    public class MrsSpaPolygon : BaseDeletableModel<long>
    {
        public MrsSpaPolygon()
        {
            CreatedOn = DateTime.UtcNow;

            this.Locations = new List<MrsSpaLocation>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public TypePolygon PolygonType { get; set; }

        [Required]
        public string UserId { get; set; }
        public MrsSpaUser User { get; set; }

        public ICollection<MrsSpaLocation> Locations { get; set; }

    }
}
