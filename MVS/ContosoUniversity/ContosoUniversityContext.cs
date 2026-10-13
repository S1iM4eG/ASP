using Microsoft.EntityFrameworkCore;

public class ContosoUniversityContext(DbContextOptions<ContosoUniversityContext> options) : DbContext(options)
{
    public DbSet<ContosoUniversity.Student> Student { get; set; } = default!;
}
