using Intsof.Exam.Domain.Users;
using Intsof.Exam.EfCore.DbContext;
using Intsof.Exam.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region EfCore

builder.Services.AddDbContext<AppDbContext>(q => q.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

#endregion

#region Repositories

builder.Services.AddTransient<IUserRepository, UserRepository>();

#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
{
    using (var scope = app.Services.CreateScope())
    {
        if (scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.GetPendingMigrations().Any())
        {
            scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
        }

    }
}

app.Run();

public partial class Program
{
    
}


