using Microsoft.EntityFrameworkCore;
using Academy.Models;

namespace Academy.Data
{
    public class AcademyContext : DbContext
    {
        public AcademyContext(DbContextOptions<AcademyContext> options) : base(options)
        {
        }
            public DbSet<Directions> Directions { get; set; }
    }
}