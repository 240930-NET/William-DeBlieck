// src/GreaterGrades/Repositories/IClassRepository.cs
using System;
using System.Collections.Generic;
using GreaterGrades.Models;

namespace GreaterGrades.Repositories
{
    public interface IClassRepository
    {
        IEnumerable<Class> GetAll();
        Class GetById(Guid id);
        void Add(Class newClass);
        void Update(Class existingClass);
        void Delete(Guid id);
    }
}
