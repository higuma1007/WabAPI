using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WabAPI.Models;
//using WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Server=127.0.0.1;Database=webapidb;User=root;Password=;";

var serverVersion = new MySqlServerVersion(new Version(8, 2, 0));

// Add services to the container.

// コンテキストの登録
builder.Services.AddDbContext<UsersContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WebAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
