// src/GreaterGrades/Repositories/IGradeRepository.cs
using System;
using System.Collections.Generic;
using GreaterGrades.Models;

namespace GreaterGrades.Repositories
{
    public interface IGradeRepository
    {
        IEnumerable<Grade> GetAll();
        Grade GetById(Guid id);
        IEnumerable<Grade> GetByStudentId(Guid studentId);
        IEnumerable<Grade> GetByAssignmentId(Guid assignmentId);
        void Add(Grade grade);
        void Update(Grade grade);
        void Delete(Guid id);
    }
}
