using DeskJr.Common;
using DeskJr.Data;
using DeskJr.Middlewares;
using DeskJr.Repository.Abstract;
using DeskJr.Repository.Concrete;
using DeskJr.Service.Abstract;
using DeskJr.Service.Concrete;
using DeskJr.Service.Dto;
using DeskJr.Service.Mapping;
using DeskJr.Services.Concrete;
using DeskJr.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Authentication Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
        };
    });

// Dependency Injection
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IOrganizationUnitService, OrganizationUnitService>();
builder.Services.AddScoped<IEmployeeTitleRepository, EmployeeTitleRepository>();
builder.Services.AddScoped<IEmployeeTitleService, EmployeeTitleService>();
builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<IHolidayService, HolidayService>();
builder.Services.AddScoped<IHolidayRepository, HolidayRepository>();
builder.Services.AddScoped<ISettingRepository, SettingRepository>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ISurveyQuestionRepository, SurveyQuestionRepository>();
builder.Services.AddScoped<ISurveyQuestionService, SurveyQuestionService>();
builder.Services.AddScoped<ISurveyQuestionOptionsRepository, SurveyQuestionOptionsRepository>();
builder.Services.AddScoped<ISurveyQuestionsOptionsService, SurveyQuestionOptionsService>();
builder.Services.AddScoped<IEmployeeOptionsRepository, EmployeeOptionsRepository>();
builder.Services.AddScoped<IEmployeeOptionsService, EmployeeOptionsService>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<EmailSender>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUserService, UserService>();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Host.UseSerilog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomMiddleware>();

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

ConfigureSerilog(builder);


app.Lifetime.ApplicationStarted.Register(() =>
{
    Log.Information("Application started");
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        await SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while migrating or seeding the database.");
    }
}

app.Run();
void ConfigureSerilog(WebApplicationBuilder builder)
{
    var columnOptions = new ColumnOptions
    {
        AdditionalColumns = new Collection<SqlColumn>
        {
            new SqlColumn { ColumnName = "LogMessage", DataType = SqlDbType.NVarChar, DataLength = -1 },
            new SqlColumn { ColumnName = "RequestMethod", DataType = SqlDbType.NVarChar, DataLength = 1024 },
            new SqlColumn { ColumnName = "RequestBody", DataType = SqlDbType.NVarChar, DataLength = -1 },
            new SqlColumn { ColumnName = "ResponseBody", DataType = SqlDbType.NVarChar, DataLength = -1 },
            new SqlColumn { ColumnName = "RequestUrl", DataType = SqlDbType.NVarChar, DataLength = 1024 },
            new SqlColumn { ColumnName = "StatusCode", DataType = SqlDbType.Int },
            new SqlColumn { ColumnName = "Ip", DataType = SqlDbType.NVarChar, DataLength = 4000 },
        }
    };
    columnOptions.Store.Remove(StandardColumn.Properties);
    columnOptions.Store.Remove(StandardColumn.MessageTemplate);
    columnOptions.Store.Remove(StandardColumn.Message);

    var logConfiguration = new LoggerConfiguration()
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .Enrich.WithClientIp();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (!string.IsNullOrEmpty(connectionString))
    {
        logConfiguration.WriteTo.MSSqlServer(
            connectionString: connectionString,
            sinkOptions: new MSSqlServerSinkOptions
            {
                AutoCreateSqlTable = true,
                TableName = "Logs"
            },
            columnOptions: columnOptions,
            restrictedToMinimumLevel: LogEventLevel.Information);
    }
    else
    {
        Log.Warning("Database connection string is empty. Logging to database is disabled.");
    }

    Log.Logger = logConfiguration.CreateLogger();
}