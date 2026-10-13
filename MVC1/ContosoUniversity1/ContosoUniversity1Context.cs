using Microsoft.EntityFrameworkCore;
public class ContosoUniversityContext(DbContextOptions<ContosoUniversityContext> options) : DbContext(options)
{
   
    public DbSet<ContosoUniversity1.Models.Student> Students { get; set; } = default!;
    public DbSet<ContosoUniversity1.Models.Course> Courses { get; set; } = default!;
    public DbSet<ContosoUniversity1.Models.Enrollment> Enrollments { get; set; } = default!;
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Student>().ToTable("Student");
    //    modelBuilder.Entity<Course>().ToTable("Course");
    //    modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
    //}
}