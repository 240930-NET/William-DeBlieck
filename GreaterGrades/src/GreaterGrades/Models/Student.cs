// src/GreaterGrades/Models/Student.cs
using System;
using System.Collections.Generic;

namespace GreaterGrades.Models
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Grade> Grades { get; set; } = new List<Grade>();

        // Constructor
        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        // Methods to add, edit, delete grades can be added here
    }
}
