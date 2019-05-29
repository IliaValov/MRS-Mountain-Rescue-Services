using System.ComponentModel.DataAnnotations;

namespace MRS.Models
{
    public class MrsImergencyMessage
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Context { get; set; }

        [Required]        
        public int LocationId { get; set; }
        public MrsLocation Location { get; set; }
    }
}