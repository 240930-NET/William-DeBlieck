using System;
using System.Collections.Generic;
using GreaterGrades.Repositories;

namespace GreaterGrades.Models
{
    public class Assignment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int MaxScore { get; set; }
        public Guid ClassId {get; set;}
        public Assignment(string name, int maxScore, Guid classId)
        {
            Name = name;
            MaxScore = maxScore;
            ClassId = classId;
            
        }

        public List<Grade> GetGrades(IGradeRepository gradeRepo) {
            var grades = gradeRepo.GetAll()
                .Where(g => g.AssignmentId == this.Id)
                .ToList();

            return grades;
        }
    }
}
