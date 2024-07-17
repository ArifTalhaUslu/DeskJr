using DeskJr.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DeskJr.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<EmployeeTitle> EmployeeTitles { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(d => d.Name).HasColumnType("VARCHAR").HasMaxLength(150).IsRequired();
            modelBuilder.Entity<EmployeeTitle>()
          .HasIndex(t => t.TitleName)
          .IsUnique();

            
        }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=test1;User Id=SA;Password=Ncc-1701;Integrated Security=False;Encrypt=False");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        }*/
    }
}
