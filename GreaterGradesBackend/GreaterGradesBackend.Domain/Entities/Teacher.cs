using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GreaterGradesBackend.Domain.Entities
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public ICollection<Class> Classes { get; set; }

        [NotMapped] 
        public IEnumerable<int> ClassIds => Classes.Select(c => c.ClassId);

        // Navigation property
        public virtual User User { get; set; }
        public Teacher()
        {
            Classes = new HashSet<Class>();
        }
    }
}