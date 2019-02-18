using Microsoft.EntityFrameworkCore;

namespace A350CEM_Course_Work.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Aircraft> Aircraft { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
