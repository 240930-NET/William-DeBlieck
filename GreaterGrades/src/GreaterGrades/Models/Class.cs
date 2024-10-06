// src/GreaterGrades/Models/Student.cs
using System;
using System.Collections.Generic;

namespace GreaterGrades.Models
{
    public class Class 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Subject { get; set; }
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<Student> Students { get; set; } = new List<Student>();

        // Constructor
        public Class(string subject)
        {
            Subject = subject;
        }
    }
}
