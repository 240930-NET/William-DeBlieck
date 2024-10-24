using System;

namespace GreaterGrades.Models
{
    public class Grade
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid StudentId { get; set; }
        public Guid AssignmentId { get; set; }
        public int? Score {get; set;} 

        public Grade(Guid studentId, Guid assignmentId)
        {
            StudentId = studentId;
            AssignmentId = assignmentId;
            Score = null;
        }
    }
}
