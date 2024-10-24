using System;
using System.Collections.Generic;

namespace GreaterGrades.Models
{
    public class Assignment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int MaxScore { get; set; }
        public List<Guid> Grades { get; set; } = new List<Guid>();

        public Assignment(string name, int maxScore)
        {
            Name = name;
            MaxScore = maxScore;
        }
    }
}
