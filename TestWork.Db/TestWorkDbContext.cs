using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestWork.Db.Models;

namespace TestWork.Db
{
    public class TestWorkDbContext : DbContext
    {
        public TestWorkDbContext()
        {
        }

        public TestWorkDbContext(DbContextOptions<TestWorkDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeModel> Employees { get; set; }

        public DbSet<GroupModel> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dalAssembly = Assembly.GetAssembly(GetType());
            modelBuilder.ApplyConfigurationsFromAssembly(dalAssembly);
        }
    }
}