using Microsoft.EntityFrameworkCore;

namespace Assignment4_EmployeeData.DAL
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Models.DBEntities.Employee> Employees { get; set; }
    }
}
