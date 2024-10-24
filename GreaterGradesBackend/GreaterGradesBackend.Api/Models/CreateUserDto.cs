using System.ComponentModel.DataAnnotations;

namespace GreaterGradesBackend.Api.Models
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [MaxLength(50)]
        public string Role { get; set; }
    }
}