using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Marks>()
            .HasKey(m => new { m.StudentId, m.CourseId, m.SubjectId });
            modelBuilder.Entity<StudentPrediction>()
        .HasKey(sp => new { sp.StudentId, sp.CourseId, sp.SubjectId });
        }

        public DbSet<WebApplication2.Models.Marks>? Marks { get; set; }
        public DbSet<StudentPrediction> StudentPredictions { get; set; }
    }
    }
