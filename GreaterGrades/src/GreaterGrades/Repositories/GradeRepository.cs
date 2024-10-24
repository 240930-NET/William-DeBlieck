// src/GreaterGrades/Repositories/GradeRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using GreaterGrades.Models;
using GreaterGrades.Services;

namespace GreaterGrades.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly IStorageService _storageService;
        private List<Grade> _grades;

        public GradeRepository(IStorageService storageService)
        {
            _storageService = storageService;
            _grades = _storageService.LoadData<Grade>();
        }

        public IEnumerable<Grade> GetAll()
        {
            return _grades;
        }

        public Grade GetById(Guid id)
        {
            return _grades.FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Grade> GetByStudentId(Guid studentId)
        {
            return _grades.Where(g => g.StudentId == studentId).ToList();
        }

        public IEnumerable<Grade> GetByAssignmentId(Guid assignmentId)
        {
            return _grades.Where(g => g.AssignmentId == assignmentId).ToList();
        }

        public void Add(Grade grade)
        {
            _grades.Add(grade);
            _storageService.SaveData(_grades);
        }

        public void Update(Grade grade)
        {
            var existing = GetById(grade.Id);
            if (existing != null)
            {
                existing.StudentId = grade.StudentId;
                existing.AssignmentId = grade.AssignmentId;
                existing.Score = grade.Score;
                // Update other properties if needed
                _storageService.SaveData(_grades);
            }
        }

        public void Delete(Guid id)
        {
            var grade = GetById(id);
            if (grade != null)
            {
                _grades.Remove(grade);
                _storageService.SaveData(_grades);
            }
        }
    }
}
