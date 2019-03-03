using Microsoft.EntityFrameworkCore;

namespace A350CEM_Course_Work.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Aircraft> Aircraft { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
