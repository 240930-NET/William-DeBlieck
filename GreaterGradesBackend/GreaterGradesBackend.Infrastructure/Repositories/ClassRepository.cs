using GreaterGradesBackend.Domain.Entities;
using GreaterGradesBackend.Domain.Interfaces;
using GreaterGradesBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GreaterGradesBackend.Infrastructure.Repositories
{
    public class ClassRepository : Repository<Class>, IClassRepository
    {
        public ClassRepository(GreaterGradesBackendDbContext context) : base(context)
        {
        }

        public async Task<Class> GetClassWithDetailsAsync(int classId)
        {
            return await _context.Classes
                .Include(c => c.Students)
                .Include(c => c.Assignments)
                .FirstOrDefaultAsync(c => c.ClassId == classId);
        }

        public async Task<List<Class>> GetAllClassesWithDetailsAsync()
        {
            return await _context.Classes
                .Include(c => c.Students)
                .Include(c => c.Assignments)
                .ToListAsync();
        }
    }
}
