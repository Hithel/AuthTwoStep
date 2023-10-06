

using System.ComponentModel.DataAnnotations;

namespace ApiAuth.Dtos
{
    public class LoginDto
    {
        
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}