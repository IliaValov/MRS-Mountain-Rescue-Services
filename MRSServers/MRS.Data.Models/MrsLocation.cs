using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MRS.Data.Models
{
    public class MrsLocation
    {
        public MrsLocation()
        {
            this.Id = System.Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        [Required]
        public virtual string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public string MessageId { get; set; }
        public MrsMessage Message { get; set; }
    }
}
