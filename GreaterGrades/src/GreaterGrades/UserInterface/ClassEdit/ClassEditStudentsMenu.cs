// src/GreaterGrades/UserInterface/Edit/ClassEditStudentsMenu.cs
using System;
using GreaterGrades.Repositories;

namespace GreaterGrades.UserInterface.Edit
{
    public class ClassEditStudentsMenu
    {
        private readonly IClassRepository _classRepository;

        public ClassEditStudentsMenu(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public void DisplayStudentsMenu(Guid classId)
        {
            // Implement student editing logic here
            Console.WriteLine($"Editing students for class ID: {classId}");
            // Add options and functionality for editing students
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }
    }
}
