// src/GreaterGrades/UserInterface/Edit/ClassEditMenu.cs
using System;
using GreaterGrades.Models;
using GreaterGrades.Repositories;

namespace GreaterGrades.UserInterface.ClassEdit
{
    public class ClassEditMenu
    {
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly ClassEditAssignmentsMenu _assignmentsMenu;
        private readonly ClassEditStudentsMenu _studentsMenu;

        public ClassEditMenu(IClassRepository classRepository, IStudentRepository studentRepository, IAssignmentRepository assignmentRepository, IGradeRepository gradeRepository)
        {
            _classRepository = classRepository;
            _studentRepository = studentRepository;
            _assignmentRepository = assignmentRepository;
            _gradeRepository = gradeRepository;
            _assignmentsMenu = new ClassEditAssignmentsMenu(classRepository, _assignmentRepository, _gradeRepository, _studentRepository);
            _studentsMenu = new ClassEditStudentsMenu(classRepository, _studentRepository, _gradeRepository, _assignmentRepository);
        }

        public void DisplayClassEditMenu(Guid classId)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Edit Class ===");
                Console.WriteLine("1. Edit Subject");
                Console.WriteLine("2. Edit Assignments");
                Console.WriteLine("3. Edit Students");
                Console.WriteLine("4. Back to Class Menu");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        EditSubject(classId);
                        break;
                    case "2":
                        _assignmentsMenu.DisplayAssignmentsMenu(classId);
                        break;
                    case "3":
                        _studentsMenu.DisplayStudentsMenu(classId);
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

        private void EditSubject(Guid classId) {
            var existingClass = _classRepository.GetById(classId);
            Console.Write($"Subject ({existingClass.Subject}): ");
            var subject = Console.ReadLine();

            existingClass.Subject = string.IsNullOrWhiteSpace(subject) ? existingClass.Subject : subject;

            _classRepository.Update(existingClass);

            Console.WriteLine("Class subject updated successfully! Press any key to return.");
        }

    }
}
