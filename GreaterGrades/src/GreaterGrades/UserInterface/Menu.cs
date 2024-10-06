// src/GreaterGrades/UserInterface/Menu.cs
using System;
using GreaterGrades.UserInterface;

namespace GreaterGrades.UserInterface
{
    public class Menu
    {
        private readonly StudentMenu _studentMenu;
        private readonly ClassMenu _classMenu;

        public Menu(StudentMenu studentMenu, ClassMenu classMenu)
        {
            _studentMenu = studentMenu;
            _classMenu = classMenu;
        }

        public void DisplayMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GreaterGrades Main Menu ===");
                Console.WriteLine("1. Manage Students");
                Console.WriteLine("2. Manage Classes");
                Console.WriteLine("9. Exit");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _studentMenu.DisplayStudentMenu();
                        break;
                    case "2":
                        _classMenu.DisplayClassMenu();
                        break;
                    case "9":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
