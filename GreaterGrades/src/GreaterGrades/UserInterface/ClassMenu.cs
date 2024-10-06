// src/GreaterGrades/UserInterface/ClassMenu.cs
using System;
using GreaterGrades.Repositories;
using GreaterGrades.UserInterface.Edit;
using GreaterGrades.Models;

namespace GreaterGrades.UserInterface
{
    public class ClassMenu
    {
        private readonly IClassRepository _classRepository;
        private readonly ClassEditMenu _classEditMenu;

        public ClassMenu(IClassRepository classRepository)
        {
            _classRepository = classRepository;
            _classEditMenu = new ClassEditMenu(_classRepository);
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
                        Console.Write("Enter Class ID to edit: ");
                        var idInput = Console.ReadLine();
                        if (Guid.TryParse(idInput, out Guid classId))
                        {
                            _classEditMenu.DisplayEditMenu(classId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID format. Press any key to return.");
                            Console.ReadKey();
                        }
                        break;
                    case "3":
                        // DeleteClass();
                        break;
                    case "4":
                        // ViewAllClasses();
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

    }
}
