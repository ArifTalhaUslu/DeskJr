using DeskJr.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace DeskJr.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<EmployeeTitle> EmployeeTitles { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyQuestionOptions> SurveyQuestionOptions { get; set; }
        public DbSet<EmployeeOptions> EmployeeOptions { get; set; }
        public DbSet<Log> Logs { get; set; }


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
                        .OnDelete(DeleteBehavior.NoAction); // Silme davranışını belirlemek önemli
            
            modelBuilder.Entity<Leave>()
                .HasOne(l => l.RequestingEmployee)
                .WithMany()
                .HasForeignKey(l => l.RequestingEmployeeId);

            modelBuilder.Entity<Leave>()
                .HasOne(l => l.ApprovedBy)
                .WithMany()
                .HasForeignKey(l => l.ApprovedById);

            // Log tablosunu EF migration'larına dahil etmemek için:
            modelBuilder.Entity<Log>().ToTable("Logs", t => t.ExcludeFromMigrations());

            // Bu tabloya sadece okuma izni verebilirsin, EF'in tabloya veri yazmasını engellemek için:
            modelBuilder.Entity<Log>().HasNoKey();  // Eğer bir primary key yoksa

        }
    }
}
