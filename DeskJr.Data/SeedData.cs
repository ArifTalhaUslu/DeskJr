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
                            HireDate = DateTime.Now.AddYears(-4),
                            Gender = Entity.Types.EnumGender.Male,
                            EmployeeRole = Entity.Types.EnumRole.Administrator,
                            Email = "admin@deskjr.com",
                            Password = "202CB962AC59075B964B07152D234B70",
                            TeamId = team.ID,
                            EmployeeTitleId = title.ID,
                            Base64Image = ""
                        });

                        var surveyId = Guid.NewGuid();
                        var survey = context.Surveys.Add(new Survey()
                        {
                            ID = surveyId,
                            Name = "Teknoloji Kullanım Alışkanlıkları",
                            EndDate = DateTime.UtcNow.AddMonths(1),
                            SurveyQuestions = new List<SurveyQuestion>
                            {
                                new SurveyQuestion
                                {
                                    ID = Guid.NewGuid(),
                                    SurveyId = surveyId,
                                    Text = "Hangi tür cihazı en çok kullanıyorsunuz?",
                                    SurveyQuestionOptions = new List<SurveyQuestionOptions>
                                    {
                                        new SurveyQuestionOptions { ID = Guid.NewGuid(), Text = "Bilgisayar" },
                                        new SurveyQuestionOptions { ID = Guid.NewGuid(), Text = "Akıllı Telefon" },
                                        new SurveyQuestionOptions { ID = Guid.NewGuid(), Text = "Tablet" }
                                    }
                                },
                                new SurveyQuestion
                                {
                                    ID = Guid.NewGuid(),
                                    SurveyId = surveyId,
                                    Text = "Günlük internet kullanım süreniz nedir?",
                                    SurveyQuestionOptions = new List<SurveyQuestionOptions>
                                    {
                                        new SurveyQuestionOptions { ID = Guid.NewGuid(), Text = "1 saatten az" },
                                        new SurveyQuestionOptions { ID = Guid.NewGuid(), Text = "1-3 saat arası" },
                                        new SurveyQuestionOptions { ID = Guid.NewGuid(), Text = "3 saatten fazla" }
                                    }
                                },
                                new SurveyQuestion
                                {
                                    ID = Guid.NewGuid(),
                                    SurveyId = surveyId,
                                    Text = "Hangi sosyal medya platformunu en çok kullanıyorsunuz?",
                                    SurveyQuestionOptions = new List<SurveyQuestionOptions>
                                    {
                                        new SurveyQuestionOptions { ID = Guid.NewGuid(), Text = "Instagram" },
                                        new SurveyQuestionOptions { ID = Guid.NewGuid(), Text = "Twitter" },
                                        new SurveyQuestionOptions { ID = Guid.NewGuid(), Text = "Facebook" }
                                    }
                                }
                            }
                        });
                       
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}

