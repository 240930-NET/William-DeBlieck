using System.Collections.Generic;

namespace GreaterGradesBackend.Api.Models
{
    public class AssignmentDto
    {
        public int AssignmentId { get; set; }
        public string Name { get; set; }

        public ClassDto Class { get; set; }

        public ICollection<GradeDto> Grades { get; set; }
    }
}
