// src/GreaterGrades/Repositories/StudentRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using GreaterGrades.Models;
using GreaterGrades.Services;

namespace GreaterGrades.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IStorageService _storageService;
        private readonly string _filePath = "data/students.json";
        private List<Student> _students;

        public StudentRepository(IStorageService storageService)
        {
            _storageService = storageService;
            _students = _storageService.LoadData<Student>();
        }

        public IEnumerable<Student> GetAll()
        {
            return _students;
        }

        public Student GetById(Guid id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Student student)
        {
            _students.Add(student);
            _storageService.SaveData(_students);
        }

        public void Update(Student student)
        {
            var existing = GetById(student.Id);
            if (existing != null)
            {
                existing.FirstName = student.FirstName;
                existing.LastName = student.LastName;
                // Update other properties as needed
                _storageService.SaveData(_students);
            }
        }

        public void Delete(Guid id)
        {
            var student = GetById(id);
            if (student != null)
            {
                _students.Remove(student);
                _storageService.SaveData(_students);
            }
        }
    }
}
