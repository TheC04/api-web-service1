using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Xml.Linq;
using api_web_service1.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<TodoContext>(opt => opt.UseMySql(builder.Configuration.GetConnectionString("api-web-serviceDb")));


IConfiguration configuration = new ConfigurationBuilder()
   .AddJsonFile("appsettings.json", true, true)
   .Build();
//MySqlConnection conn = new MySqlConnection("server=localhost;user id=todoapp;password=todoapp;database=todoapp");
string? s = configuration.GetConnectionString("api-web-service");
Console.WriteLine(s);
builder.Services.AddDbContext<PostContext>(options => options.UseMySql(s, ServerVersion.AutoDetect(s)));
builder.Services.AddDbContext<UserContext>(options => options.UseMySql(s, ServerVersion.AutoDetect(s)));
builder.Services.AddDbContext<FollowContext>(options => options.UseMySql(s, ServerVersion.AutoDetect(s)));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();