using EmployeeAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Infrastructure.Data;

public class EmployeeDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Employee>().HasData(
            Enumerable.Range(1, 100).Select(i => new Employee
            {
                Id = i,
                FirstName = $"FirstName{i}",
                LastName = $"LastName{i}",
                Email = $"email{i}@example.com",
                DateOfBirth = DateTime.Now.AddYears(-30).AddDays(i)
            }).ToArray()
        );
    }
}

