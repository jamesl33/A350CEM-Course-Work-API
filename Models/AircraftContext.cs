using Microsoft.EntityFrameworkCore;

namespace A350CEM_Course_Work.Models
{
    public class AircraftContext : DbContext
    {
        public AircraftContext(DbContextOptions<AircraftContext> options) : base(options) {}

        public DbSet<Aircraft> Aircraft { get; set; }
    }
}
