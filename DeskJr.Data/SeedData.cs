using DeskJr.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeskJr.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {

                if (!context.Employees.Any())
                {
                    var teamId = Guid.NewGuid();
                    context.Teams.Add(new Team()
                    {
                        Name = "Default",
                        ID = teamId,
                    });
                    await context.SaveChangesAsync();

                    var titleId = Guid.NewGuid();
                    context.EmployeeTitles.Add(new EmployeeTitle()
                    {
                        TitleName = "Default Title",
                        ID = titleId,
                    });
                    await context.SaveChangesAsync();

                    var team = context.Teams.FirstOrDefault(t => t.Name == "Default");
                    var title = context.EmployeeTitles.FirstOrDefault(et => et.TitleName == "Default Title");

                    if (team != null && title != null)
                    {
                        var employeeId = Guid.NewGuid();
                        context.Employees.Add(new Employee()
                        {
                            ID = employeeId,
                            Name = "Admin",
                            DayOfBirth = DateTime.Now.AddYears(-23),
                            Gender = Entity.Types.EnumGender.Male,
                            EmployeeRole = Entity.Types.EnumRole.Administrator,
                            Email = "admin@deskjr.com",
                            Password = "202CB962AC59075B964B07152D234B70",
                            TeamId = team.ID,
                            EmployeeTitleId = title.ID,
                            Base64Image = ""
                        });

                        await context.SaveChangesAsync();
                    }
                }             
            }
        }
    }
}

