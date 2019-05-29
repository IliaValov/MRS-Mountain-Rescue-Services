using System;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models
{
    public class MrsUserVerification
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public DateTime ExpiredDate { get; set; } = DateTime.UtcNow.AddMinutes(5);
    }
}