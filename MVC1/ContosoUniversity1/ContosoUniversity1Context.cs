using ContosoUniversity1.Models;
using Microsoft.EntityFrameworkCore;

public class ContosoUniversity1Context : DbContext
{
    public ContosoUniversity1Context(DbContextOptions<ContosoUniversity1Context> options) : base(options) { }

        public DbSet<ContosoUniversity1.Models.Student> Students { get; set; } = default!;
        public DbSet<ContosoUniversity1.Models.Course> Courses { get; set; } = default!;
        public DbSet<ContosoUniversity1.Models.Enrollment> Enrollments { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().ToTable("Student");
        modelBuilder.Entity<Course>().ToTable("Course");
        modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
    }

}