using Microsoft.EntityFrameworkCore;

namespace CoreMvcMasterDetailCrud.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public virtual DbSet<Course>Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<CourseModule> CourseModules { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
