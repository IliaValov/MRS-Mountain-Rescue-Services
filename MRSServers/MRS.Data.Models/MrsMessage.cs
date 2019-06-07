using System;
using System.ComponentModel.DataAnnotations;

namespace MRS.Data.Models
{
    public class MrsMessage
    {
        public MrsMessage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string LocatioId { get; set; }
        public MrsLocation Location { get; set; }
    }
}
