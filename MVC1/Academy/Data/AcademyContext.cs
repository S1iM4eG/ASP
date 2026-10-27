using Microsoft.EntityFrameworkCore;
using Academy.Models;

namespace Academy.Data
{
    public class AcademyContext : DbContext
    {
        public DbSet<Academy.Models.Groups> Groups { get; set; } = default!;
        public DbSet<Academy.Models.Teachers> Teachers { get; set; } = default!;
        public DbSet<Academy.Models.Disciplines> Disciplines { get; set; } = default!;
        public DbSet<Academy.Models.Students> Students { get; set; } = default!;
        public AcademyContext(DbContextOptions<AcademyContext> options) : base(options) { }
        public DbSet<Directions> Directions { get; set; }
    }
}