using System.ComponentModel.DataAnnotations;

namespace GreaterGradesBackend.Api.Models
{
    public class CreateClassDto
    {
        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }
    }
}
