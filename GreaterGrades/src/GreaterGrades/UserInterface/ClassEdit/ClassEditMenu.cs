// src/GreaterGrades/UserInterface/Edit/ClassEditMenu.cs
using System;
using GreaterGrades.Repositories;

namespace GreaterGrades.UserInterface.Edit
{
    public class ClassEditMenu
    {
        private readonly IClassRepository _classRepository;
        private readonly ClassEditAssignmentsMenu _assignmentsMenu;
        private readonly ClassEditStudentsMenu _studentsMenu;

        public ClassEditMenu(IClassRepository classRepository)
        {
            _classRepository = classRepository;
            _assignmentsMenu = new ClassEditAssignmentsMenu(classRepository);
            _studentsMenu = new ClassEditStudentsMenu(classRepository);
        }

        public void DisplayEditMenu(Guid classId)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Edit Class ===");
                Console.WriteLine("1. Edit Assignments");
                Console.WriteLine("2. Edit Students");
                Console.WriteLine("3. Back to Class Menu");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _assignmentsMenu.DisplayAssignmentsMenu(classId);
                        break;
                    case "2":
                        _studentsMenu.DisplayStudentsMenu(classId);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
