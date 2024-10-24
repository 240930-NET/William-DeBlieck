using System;
using System.Collections.Generic;
using System.Linq;
using GreaterGrades.Models;
using GreaterGrades.Services;

namespace GreaterGrades.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly IStorageService _storageService;
        private List<Assignment> _assignments;

        public AssignmentRepository(IStorageService storageService)
        {
            _storageService = storageService;
            _assignments = _storageService.LoadData<Assignment>();
        }

        public IEnumerable<Assignment> GetAll()
        {
            return _assignments;
        }

        public Assignment GetById(Guid id)
        {
            return _assignments.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Assignment assignment)
        {
            _assignments.Add(assignment);
            _storageService.SaveData(_assignments);
        }

        public void Update(Assignment assignment)
        {
            var existing = GetById(assignment.Id);
            if (existing != null)
            {
                existing.Name = assignment.Name;
                existing.MaxScore = assignment.MaxScore;
                existing.Grades = assignment.Grades; // Update grades
                _storageService.SaveData(_assignments);
            }
        }

        public void Delete(Guid id)
        {
            var assignment = GetById(id);
            if (assignment != null)
            {
                _assignments.Remove(assignment);
                _storageService.SaveData(_assignments);
            }
        }
    }
}
