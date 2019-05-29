using System;
using System.ComponentModel.DataAnnotations;

namespace MRS.Models
{
    public class MrsUserAuthanticationToken
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public int UserId { get; set; }
        public MrsUser User { get; set; }
    }
}