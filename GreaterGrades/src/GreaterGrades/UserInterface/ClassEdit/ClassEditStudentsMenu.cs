using System;
using System.Linq;
using GreaterGrades.Repositories;
using GreaterGrades.Models;

namespace GreaterGrades.UserInterface.ClassEdit
{
    public class ClassEditStudentsMenu
    {
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;
        
        private readonly IGradeRepository _gradeRepository;
        private readonly IAssignmentRepository _assignmentRepository;

        public ClassEditStudentsMenu(IClassRepository classRepository, IStudentRepository studentRepository, IGradeRepository gradeRepository, IAssignmentRepository assignmentRepository)
        {
            _classRepository = classRepository;
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
            _assignmentRepository = assignmentRepository;
        }

        public void DisplayStudentsMenu(Guid classId)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Edit Students for Class ID: {classId} ===");
                Console.WriteLine("1. Add Student to Class");
                Console.WriteLine("2. Remove Student from Class");
                Console.WriteLine("3. View All Students in Class");
                Console.WriteLine("4. Back to Edit Class Menu");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddStudentToClass(classId);
                        break;
                    case "2":
                        RemoveStudentFromClass(classId);
                        break;
                    case "3":
                        ViewAllStudentsInClass(classId);
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

        private void AddStudentToClass(Guid classId)
        {
            Console.Clear();
            Console.WriteLine("=== Add Student to Class ===");

            var allStudents = _studentRepository.GetAll().ToList();
            if (!allStudents.Any())
            {
                Console.WriteLine("No students available to add. Press any key to return.");
                Console.ReadKey();
                return;
            }

            // Sort students alphabetically or by another criterion if needed
            allStudents = allStudents.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();

            Console.WriteLine("Available Students:");
            for (int i = 0; i < allStudents.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Name: {allStudents[i].FirstName} {allStudents[i].LastName}");
            }

            Console.Write("Enter the number of the student to add: ");
            var studentNumberInput = Console.ReadLine();

            if (!int.TryParse(studentNumberInput, out int studentNumber) || studentNumber < 1 || studentNumber > allStudents.Count)
            {
                Console.WriteLine("Invalid number. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var studentToAdd = allStudents[studentNumber - 1];

            var classToUpdate = _classRepository.GetById(classId);
            if (classToUpdate == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            if (!classToUpdate.AddStudent(studentToAdd.Id, _assignmentRepository, _studentRepository, _gradeRepository, _classRepository))
            {
                Console.WriteLine("Student is already enrolled in this class. Press any key to return.");
            }
            else
            {
                Console.WriteLine("Student added to class successfully! Press any key to return.");
            }

            Console.ReadKey();
        }


       private void RemoveStudentFromClass(Guid classId)
        {
            Console.Clear();
            Console.WriteLine("=== Remove Student from Class ===");

            var classToUpdate = _classRepository.GetById(classId);
            if (classToUpdate == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            if (!classToUpdate.Students.Any())
            {
                Console.WriteLine("No students enrolled in this class. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var enrolledStudents = classToUpdate.Students
                .Select(sId => _studentRepository.GetById(sId))
                .Where(student => student != null)
                .OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
                .ToList();

            Console.WriteLine("Students in Class:");
            for (int i = 0; i < enrolledStudents.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Name: {enrolledStudents[i].FirstName} {enrolledStudents[i].LastName}");
            }

            Console.Write("Enter the number of the student to remove: ");
            var studentNumberInput = Console.ReadLine();

            if (!int.TryParse(studentNumberInput, out int studentNumber) || studentNumber < 1 || studentNumber > enrolledStudents.Count)
            {
                Console.WriteLine("Invalid number. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var studentToRemove = enrolledStudents[studentNumber - 1];

            if (!classToUpdate.RemoveStudent(studentToRemove.Id))
            {
                Console.WriteLine("Student is not enrolled in this class. Press any key to return.");
            }
            else
            {
                _classRepository.Update(classToUpdate);
                Console.WriteLine("Student removed from class successfully! Press any key to return.");
            }

            Console.ReadKey();
        }


        private void ViewAllStudentsInClass(Guid classId)
        {
            Console.Clear();
            Console.WriteLine("=== Students in Class ===");

            var classToView = _classRepository.GetById(classId);
            if (classToView == null)
            {
                Console.WriteLine("Class not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            if (!classToView.Students.Any())
            {
                Console.WriteLine("No students enrolled in this class.");
            }
            else
            {
                Console.WriteLine("Students Enrolled:");
                foreach (var studentId in classToView.Students)
                {
                    var student = _studentRepository.GetById(studentId);
                    Console.WriteLine($"ID: {student.Id} | Name: {student.FirstName} {student.LastName}");
                }
            }

            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }
    }
}
