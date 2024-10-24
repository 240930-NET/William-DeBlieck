using System;
using System.Collections.Generic;
using System.Linq;
using GreaterGrades.Repositories;

namespace GreaterGrades.Models
{
    public class Class
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Subject { get; set; }
        public List<Guid> Assignments { get; set; } = new List<Guid>();
        public List<Guid> Students { get; set; } = new List<Guid>();

        public Class(string subject)
        {
            Subject = subject;
        }

        public bool AddStudent(Guid studentId, IStudentRepository studentRepo, IGradeRepository gradeRepo)
        {
            // Check if the student already exists in the class
            if (Students.Contains(studentId))
            {
                return false; // Student already exists in the class
            }

            // Retrieve the student from the repository
            var student = studentRepo.GetById(studentId);
            if (student == null)
            {
                return false; // Student not found
            }

            // Add the student to the class
            Students.Add(studentId);

            // Add grades for all existing assignments
            foreach (var assignmentId in Assignments)
            {
                // Call the AddGrade method on the student object
                student.AddGrade(assignmentId, gradeRepo, studentRepo);
            }

            // Optionally update the student repository if needed
            studentRepo.Update(student);

            return true;
        }


        public bool RemoveStudent(Guid studentId)
        {
            if (!Students.Contains(studentId))
            {
                return false; // Student not found in the class
            }

            Students.Remove(studentId);
            return true;
        }

        public List<Student> GetStudents(IStudentRepository studentRepo)
        {
            return Students.Select(studentRepo.GetById).ToList();
        }

        public bool AddAssignment(string assignmentName, int MaxScore, IAssignmentRepository assignmentRepo, IStudentRepository studentRepo, IClassRepository classRepo, IGradeRepository gradeRepo)

        {
            Assignment newAssignment = new Assignment(assignmentName, MaxScore);

            assignmentRepo.Add(newAssignment);

            Assignments.Add(newAssignment.Id);

            foreach (Guid studentId in Students){
                Student iterStudent = studentRepo.GetById(studentId);
                iterStudent.AddGrade(newAssignment.Id, gradeRepo, studentRepo);
            }

            classRepo.Update(this);
            return true;
        }


        public bool RemoveAssignment(Guid assignmentId, IAssignmentRepository assignmentRepo, IStudentRepository studentRepo, IClassRepository classRepo, IGradeRepository gradeRepo)
        {
            if (!Assignments.Contains(assignmentId))
            {
                return false; // Assignment not found
            }

            // Remove the assignment from the class
            Assignments.Remove(assignmentId);

            // Remove the grades linked to this assignment for each student
            foreach (var studentId in Students)
            {
                var student = studentRepo.GetById(studentId);
                student.RemoveGrade(assignmentId, gradeRepo, studentRepo); // Call to the RemoveGrade method
            }

            // Now delete the assignment from the assignmentRepo
            assignmentRepo.Delete(assignmentId);
            classRepo.Update(this);

            return true; // Assignment successfully removed
        }
    }
}
