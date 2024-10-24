using System;
using GreaterGrades.Models;
using GreaterGrades.Repositories;
using System.Linq;

namespace GreaterGrades.UserInterface.ClassEdit
{
    public class ClassEditAssignmentsMenu
    {
        private readonly IClassRepository _classRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IStudentRepository _studentRepository;

        public ClassEditAssignmentsMenu(IClassRepository classRepository, IAssignmentRepository assignmentRepository, IGradeRepository gradeRepository, IStudentRepository studentRepository)
        {
            _classRepository = classRepository;
            _assignmentRepository = assignmentRepository;
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
        }

        public void DisplayAssignmentsMenu(Guid classId)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Edit Assignments for Class ID: {classId} ===");
                Console.WriteLine("1. Add Assignment to Class");
                Console.WriteLine("2. Remove Assignment from Class");
                Console.WriteLine("3. View All Assignments in Class");
                Console.WriteLine("4. Back to Edit Class Menu");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddAssignmentToClass(classId);
                        break;
                    case "2":
                        RemoveAssignmentFromClass(classId);
                        break;
                    case "3":
                        ViewAllAssignmentsInClass(classId);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AddAssignmentToClass(Guid classId)
        {
            Console.Clear();
            Console.WriteLine("=== Add Assignment to Class ===");

            var classToUpdate = _classRepository.GetById(classId);
            if (classToUpdate == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter the name of the new assignment: ");
            var assignmentName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(assignmentName))
            {
                Console.WriteLine("Assignment name cannot be empty. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter the maximum score for the assignment: ");
            var maxScoreInput = Console.ReadLine();

            if (!int.TryParse(maxScoreInput, out int maxScore) || maxScore <= 0)
            {
                Console.WriteLine("Invalid maximum score. It must be a positive integer. Press any key to return.");
                Console.ReadKey();
                return;
            }


            classToUpdate.AddAssignment(assignmentName, int.Parse(maxScoreInput), _assignmentRepository, _studentRepository, _classRepository, _gradeRepository);
            _classRepository.Update(classToUpdate);

            Console.WriteLine($"Assignment '{assignmentName}' with a maximum score of {maxScore} added to the class successfully! Press any key to return.");
            Console.ReadKey();
        }

        private void RemoveAssignmentFromClass(Guid classId)
        {
            Console.Clear();
            Console.WriteLine("=== Remove Assignment from Class ===");

            var classToUpdate = _classRepository.GetById(classId);
            if (classToUpdate == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            if (!classToUpdate.Assignments.Any())
            {
                Console.WriteLine("No assignments found in this class. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Assignments in Class:");
            foreach (var id in classToUpdate.Assignments) // Renamed variable
            {
                var assignment = _assignmentRepository.GetById(id);
                Console.WriteLine($"ID: {assignment.Id} | Name: {assignment.Name}");
            }

            Console.Write("Enter the Assignment ID to remove: ");
            var assignmentIdInput = Console.ReadLine();

            if (!Guid.TryParse(assignmentIdInput, out Guid assignmentId))
            {
                Console.WriteLine("Invalid ID format. Press any key to return.");
                Console.ReadKey();
                return;
            }

            if (!classToUpdate.Assignments.Contains(assignmentId))
            {
                Console.WriteLine("Assignment not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            classToUpdate.RemoveAssignment(assignmentId,  _assignmentRepository, _studentRepository,  _classRepository, _gradeRepository); // You can pass StudentRepository here if needed


            Console.WriteLine("Assignment removed from class successfully! Press any key to return.");
            Console.ReadKey();
        }


        private void ViewAllAssignmentsInClass(Guid classId)
        {
            Console.Clear();
            Console.WriteLine("=== Assignments in Class ===");

            var classToView = _classRepository.GetById(classId);
            if (classToView == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var assignments = classToView.Assignments;
            if (!assignments.Any())
            {
                Console.WriteLine("No assignments in this class.");
            }
            else
            {
                Console.WriteLine("Assignments in Class:");
                foreach (var Id in assignments)
                {
                    var assignment = _assignmentRepository.GetById(Id);
                    Console.WriteLine($"ID: {assignment.Id} | Name: {assignment.Name}");
                }
            }

            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }
    }
}
