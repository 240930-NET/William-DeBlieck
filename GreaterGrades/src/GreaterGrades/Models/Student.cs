using System;
using System.Collections.Generic;
using GreaterGrades.Models;
using GreaterGrades.Repositories;

namespace GreaterGrades.Models
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Guid> Grades { get; set; } = new List<Guid>();
        public List<Guid> Classes {get; set;} = new List<Guid>();

        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public bool AddGrade(Guid assignmentId, IGradeRepository gradeRepo, IStudentRepository studentRepo)
        {
            if (gradeRepo == null || studentRepo == null)
                throw new ArgumentNullException("Repository cannot be null.");

            // Ensure Grades list is initialized
            if (Grades == null)
                Grades = new List<Guid>();

            // Create a new Grade object for this student and the given assignment
            var newGrade = new Grade(Id, assignmentId);

            // Add the new grade to the repository
            gradeRepo.Add(newGrade);

            // Add the assignmentId to the student's Grades list
            Grades.Add(newGrade.Id);

            // Update the student in the repository
            studentRepo.Update(this);

            return true; // Indicate success
        }



        // Method to remove a grade by assignmentId
        public bool RemoveGrade(Guid assignmentId, IGradeRepository gradeRepo, IStudentRepository studentRepo)
        {
            // Retrieve all grades associated with this student
            var gradesForStudent = gradeRepo.GetByStudentId(Id);

            // Check if any of the grades correspond to the given assignmentId
            var gradeToRemove = gradesForStudent.FirstOrDefault(g => g.AssignmentId == assignmentId);
            if (gradeToRemove != null)
            {
                // Remove the grade from the repository
                gradeRepo.Delete(gradeToRemove.Id);
                
                // Optionally remove the assignmentId from the student's Grades list
                Grades.Remove(assignmentId); 

                return true;
            }
            studentRepo.Update(this);

            return false; // Grade not found
        }


        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}";
        }
    }
}
