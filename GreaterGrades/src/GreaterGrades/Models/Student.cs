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

        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public List<Grade> GetGrades(IGradeRepository gradeRepo)
        {
            var grades = gradeRepo.GetAll()
                .Where(g => g.StudentId == this.Id)
                .ToList();

            return grades;
        }

        public bool AddGrade(Guid assignmentId, IGradeRepository gradeRepo, IStudentRepository studentRepo)
        {
            if (gradeRepo == null || studentRepo == null)
                throw new ArgumentNullException("Repository cannot be null.");

            // Create a new Grade object for this student and the given assignment
            var newGrade = new Grade(Id, assignmentId);

            // Add the new grade to the repository
            gradeRepo.Add(newGrade);

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
