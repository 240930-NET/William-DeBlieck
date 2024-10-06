// src/GreaterGrades/Repositories/IStudentRepository.cs
using System;
using System.Collections.Generic;
using GreaterGrades.Models;

namespace GreaterGrades.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student GetById(Guid id);
        void Add(Student student);
        void Update(Student student);
        void Delete(Guid id);
    }
}
