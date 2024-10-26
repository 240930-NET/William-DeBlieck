using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreaterGradesBackend.Domain.Entities
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }

        public ICollection<User> Students { get; set; }
        public ICollection<User> Teachers { get; set; }

        public ICollection<Assignment> Assignments { get; set; }

        public Class()
        {
            Students = new HashSet<User>();
            Teachers = new HashSet<User>();
            Assignments = new HashSet<Assignment>();
        }
    }
}
