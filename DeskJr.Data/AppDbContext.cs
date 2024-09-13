using DeskJr.Entity.Models;
using Microsoft.EntityFrameworkCore;

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

            //modelBuilder.Entity<SurveyQuestionOptions>()
            //    .HasOne(m => m.SurveyQuestion)
            //    .WithMany()
            //    .HasForeignKey(m => m.SurveyQuestionId);
            
            //modelBuilder.Entity<SurveyQuestion>()
            //    .HasOne(m => m.Survey)
            //    .WithMany()
            //    .HasForeignKey(m => m.SurveyId);

            
        }
    }
}
