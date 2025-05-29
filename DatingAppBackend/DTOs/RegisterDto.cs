using System.ComponentModel.DataAnnotations;

namespace DatingAppBackend.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(100)]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
