using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GreaterGradesBackend.Domain.Entities
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public ICollection<Class> Classes { get; set; }
        public ICollection<Grade> Grades { get; set; }

        // Read-only property to get Class IDs
        [NotMapped] 
        public IEnumerable<int> ClassIds => Classes.Select(c => c.ClassId);
        [NotMapped] 
        public IEnumerable<int> GradeIds => Grades.Select(c => c.GradeId);
        public virtual User User { get; set; }

        public Student()
        {
            Classes = new HashSet<Class>();
            Grades = new HashSet<Grade>();
        }
    }
}
