using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GreaterGradesBackend.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [MaxLength(50)]
        public string Role { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public ICollection<Class> Classes { get; set; }
        public ICollection<Grade> Grades { get; set; }

        [NotMapped] 
        public IEnumerable<int> ClassIds => Classes.Select(c => c.ClassId);
        
        [NotMapped] 
        public IEnumerable<int> GradeIds => Grades.Select(g => g.GradeId);

        public User()
        {
            Role = "Student"; // Default role
            Classes = new HashSet<Class>();
            Grades = new HashSet<Grade>();
        }
    }
}
