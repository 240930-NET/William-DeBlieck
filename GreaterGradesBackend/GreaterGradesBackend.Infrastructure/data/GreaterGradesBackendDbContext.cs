using GreaterGradesBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreaterGradesBackend.Infrastructure
{
    public class GreaterGradesBackendDbContext : DbContext
    {
        public GreaterGradesBackendDbContext(DbContextOptions<GreaterGradesBackendDbContext> options)
            : base(options)
        {
        }

        //public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<User> Users { get; set; }

        // Configure entity relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Many-to-Many Relationship Configuration
            modelBuilder.Entity<User>()
                .HasMany(s => s.Classes)
                .WithMany(c => c.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentClass", // The join table name
                    sc => sc.HasOne<Class>().WithMany().HasForeignKey("ClassId"),
                    sc => sc.HasOne<User>().WithMany().HasForeignKey("UserId")
                );
        }

    }
}
