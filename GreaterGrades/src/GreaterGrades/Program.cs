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
            var studentsStorageService = new JsonStorageService("data/students.json");
            var classesStorageService = new JsonStorageService("data/classes.json");
            var assignmentStorageService = new JsonStorageService("data/assignments.json");
            var gradeStorageService = new JsonStorageService("data/grades.json");


            // Initialize repositories
            var studentRepository = new StudentRepository(studentsStorageService);
            var classRepository = new ClassRepository(classesStorageService);
            var assignmentRepository = new AssignmentRepository(assignmentStorageService);
            var gradeRepository = new GradeRepository(gradeStorageService);

            // Initialize user interface
            var studentMenu = new StudentMenu(studentRepository);
            var classMenu = new ClassMenu(classRepository, studentRepository, assignmentRepository, gradeRepository);

            var menu = new Menu(studentMenu, classMenu);

            // Start the application
            menu.DisplayMainMenu();
        }
    }
}
