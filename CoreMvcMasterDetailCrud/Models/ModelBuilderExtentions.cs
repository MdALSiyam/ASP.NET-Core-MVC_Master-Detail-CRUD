using Microsoft.EntityFrameworkCore;

namespace CoreMvcMasterDetailCrud.Models
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(new Course { CourseId = 1, CourseName = "C#" }, new Course { CourseId = 2, CourseName = "NT" },
                new Course { CourseId = 3, CourseName = "J2EE" },
                new Course { CourseId = 4, CourseName = "Wada" });
        }
    }
}
