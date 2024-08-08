using DeskJr.Entity.Models;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Holiday> Holidays { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(d => d.Name).HasColumnType("VARCHAR").HasMaxLength(150).IsRequired();
          
            modelBuilder.Entity<EmployeeTitle>()
                                      .HasIndex(t => t.TitleName)
                                      .IsUnique();

                modelBuilder.Entity<Team>()
                       .HasOne(t => t.Manager)
                       .WithMany()
                       .HasForeignKey(t => t.ManagerId)
                       .OnDelete(DeleteBehavior.Restrict); // Silme davranışını belirlemek önemli

                modelBuilder.Entity<Employee>()
                        .HasOne(e => e.Team)
                        .WithMany() // Eğer Team ile birden fazla Employee ilişkisi varsa WithMany
                        .HasForeignKey(e => e.TeamId)
                        .OnDelete(DeleteBehavior.Restrict); // Silme davranışını belirlemek önemli
            
            modelBuilder.Entity<Leave>()
                .HasOne(l => l.RequestingEmployee)
                .WithMany()
                .HasForeignKey(l => l.RequestingEmployeeId);

            modelBuilder.Entity<Leave>()
                .HasOne(l => l.ApprovedBy)
                .WithMany()
                .HasForeignKey(l => l.ApprovedById);

        }
            /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=test1;User Id=SA;Password=Ncc-1701;Integrated Security=False;Encrypt=False");
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            }*/
    }
}
