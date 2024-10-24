using System.Collections.Generic;

namespace GreaterGradesBackend.Api.Models
{
    public class ClassDto
    {
        public int ClassId { get; set; }

        public string Subject { get; set; }

        public ICollection<UserDto> Students { get; set; }

        public ICollection<AssignmentDto> Assignments { get; set; }
    }
}
