// src/GreaterGrades/UserInterface/ClassMenu.cs
using System;
using GreaterGrades.Repositories;
using GreaterGrades.UserInterface.ClassEdit;
using GreaterGrades.Models;

namespace GreaterGrades.UserInterface
{
    public class ClassMenu
    {
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ClassEditMenu _classEditMenu;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IGradeRepository _gradeRepository;

        public ClassMenu(IClassRepository classRepository, IStudentRepository studentRepository, IAssignmentRepository assignmentRepository, IGradeRepository gradeRepository){
            _classRepository = classRepository;
            _studentRepository = studentRepository;
            _assignmentRepository = assignmentRepository;
            _gradeRepository = gradeRepository;
            _classEditMenu = new ClassEditMenu(_classRepository, _studentRepository, _assignmentRepository, _gradeRepository);
        }

        public void DisplayClassMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Classes ===");
                Console.WriteLine("1. Create Class");
                Console.WriteLine("2. Edit Class");
                Console.WriteLine("3. Delete Class");
                Console.WriteLine("4. View All Classes");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateClass();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("=== Select Class to Edit ===");

                        var allClasses = _classRepository.GetAll().ToList();
                        if (!allClasses.Any())
                        {
                            Console.WriteLine("No classes available to edit. Press any key to return.");
                            Console.ReadKey();
                            return;
                        }

                        // Sort classes alphabetically by Subject (or another criterion if needed)
                        allClasses = allClasses.OrderBy(c => c.Subject).ToList();

                        Console.WriteLine("Available Classes:");
                        for (int i = 0; i < allClasses.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. Subject: {allClasses[i].Subject}");
                        }

                        Console.Write("Enter the number of the class to edit: ");
                        var classNumberInput = Console.ReadLine();

                        if (!int.TryParse(classNumberInput, out int classNumber) || classNumber < 1 || classNumber > allClasses.Count)
                        {
                            Console.WriteLine("Invalid number. Press any key to return.");
                            Console.ReadKey();
                            return;
                        }

                        var classToEdit = allClasses[classNumber - 1];

                        // Call the edit menu with the selected class ID
                        _classEditMenu.DisplayClassEditMenu(classToEdit.Id);

                        Console.ReadKey();

                        break;
                    case "3":
                        DeleteClass();
                        break;
                    case "4":
                        ViewAllClasses();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CreateClass()
        {
            Console.Clear();
            Console.WriteLine("=== Create New Class ===");
            Console.Write("Subject: ");
            var subject = Console.ReadLine();
            var newClass = new Class(subject);
            _classRepository.Add(newClass);

            Console.WriteLine("Class added successfully! Press any key to return.");
            Console.ReadKey();
        }

        private void DeleteClass()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Class ===");
            Console.Write("Enter Class ID: ");
            var idInput = Console.ReadLine();

            if (Guid.TryParse(idInput, out Guid classId))
            {
                var existingClass = _classRepository.GetById(classId);
                if (existingClass != null)
                {
                    _classRepository.Delete(classId);
                    Console.WriteLine("Class deleted successfully! Press any key to return.");
                }
                else
                {
                    Console.WriteLine("Class not found. Press any key to return.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format. Press any key to return.");
            }

            Console.ReadKey();
        }

        private void ViewAllClasses()
        {
            Console.Clear();
            Console.WriteLine("=== All Classes ===");

            var existingClasses = _classRepository.GetAll();

            foreach (var existingClass in existingClasses)
            {
                Console.WriteLine($"ID: {existingClass.Id} | Subject: {existingClass.Subject}");
            }

            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

    }
}
