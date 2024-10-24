using System;
using System.Collections.Generic;
using GreaterGrades.Models;

namespace GreaterGrades.Repositories
{
    public interface IAssignmentRepository
    {
        IEnumerable<Assignment> GetAll();
        Assignment GetById(Guid id);
        void Add(Assignment assignment);
        void Update(Assignment assignment);
        void Delete(Guid id);
    }
}
