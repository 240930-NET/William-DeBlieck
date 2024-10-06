// src/GreaterGrades/Program.cs
using System;
using GreaterGrades.Services;
using GreaterGrades.Repositories;
using GreaterGrades.UserInterface;

namespace GreaterGrades
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize storage service
            var storageService = new JsonStorageService("data/students.json"); // Example for students

            // Initialize repositories
            var studentRepository = new StudentRepository(storageService);
            var classRepository = new ClassRepository(storageService);

            // Initialize user interface
            var studentMenu = new StudentMenu(studentRepository);
            var classMenu = new ClassMenu(classRepository);

            var menu = new Menu(studentMenu, classMenu);

            // Start the application
            menu.DisplayMainMenu();
        }
    }
}
