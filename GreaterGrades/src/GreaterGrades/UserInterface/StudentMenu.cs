// src/GreaterGrades/UserInterface/StudentMenu.cs
using System;
using GreaterGrades.Repositories;
using GreaterGrades.Models;

namespace GreaterGrades.UserInterface
{
    public class StudentMenu
    {
        private readonly IStudentRepository _studentRepository;

        public StudentMenu(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void DisplayStudentMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Students ===");
                Console.WriteLine("1. Create Student");
                Console.WriteLine("2. Edit Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. View All Students");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Select an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateStudent();
                        break;
                    case "2":
                        EditStudent();
                        break;
                    case "3":
                        DeleteStudent();
                        break;
                    case "4":
                        ViewAllStudents();
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

        private void CreateStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Create New Student ===");
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();

            var student = new Student(firstName, lastName);
            _studentRepository.Add(student);

            Console.WriteLine("Student added successfully! Press any key to return.");
            Console.ReadKey();
        }

        private void EditStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Edit Student ===");
            Console.Write("Enter Student ID: ");
            var idInput = Console.ReadLine();

            if (Guid.TryParse(idInput, out Guid studentId))
            {
                var student = _studentRepository.GetById(studentId);
                if (student != null)
                {
                    Console.Write($"First Name ({student.FirstName}): ");
                    var firstName = Console.ReadLine();
                    Console.Write($"Last Name ({student.LastName}): ");
                    var lastName = Console.ReadLine();

                    student.FirstName = string.IsNullOrWhiteSpace(firstName) ? student.FirstName : firstName;
                    student.LastName = string.IsNullOrWhiteSpace(lastName) ? student.LastName : lastName;

                    _studentRepository.Update(student);

                    Console.WriteLine("Student updated successfully! Press any key to return.");
                }
                else
                {
                    Console.WriteLine("Student not found. Press any key to return.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format. Press any key to return.");
            }

            Console.ReadKey();
        }

        private void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Student ===");
            Console.Write("Enter Student ID: ");
            var idInput = Console.ReadLine();

            if (Guid.TryParse(idInput, out Guid studentId))
            {
                var student = _studentRepository.GetById(studentId);
                if (student != null)
                {
                    _studentRepository.Delete(studentId);
                    Console.WriteLine("Student deleted successfully! Press any key to return.");
                }
                else
                {
                    Console.WriteLine("Student not found. Press any key to return.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format. Press any key to return.");
            }

            Console.ReadKey();
        }

        private void ViewAllStudents()
        {
            Console.Clear();
            Console.WriteLine("=== All Students ===");

            var students = _studentRepository.GetAll();

            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id} | Name: {student.FirstName} {student.LastName}");
            }

            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }
    }
}
