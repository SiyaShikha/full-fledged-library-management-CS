using DotNetEnv;
using LibraryManagement.Data;
using LibraryManagement.Repository;
using LibraryManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement;

public class Program
{
  public static void Main(string[] args)
  { 
    var builder = WebApplication.CreateBuilder(args);

    ConfigureServices(builder);

    Env.Load();

    var app = builder.Build();

    ConfigureMiddleware(app);

    app.Run();
  }

  private static void ConfigureServices(WebApplicationBuilder builder)
  {
    builder.Services.AddDbContext<LibraryContext>(options =>
      options.UseNpgsql(builder.Configuration.GetConnectionString("LibraryConnection")));

    builder.Services.AddCors(options =>
    {
      options.AddPolicy("AllowReactApp", policy =>
      {
        policy.WithOrigins("*")
          .AllowAnyHeader()
          .AllowAnyMethod();
      });
    });

    builder.Services.AddScoped<BookService>();
    builder.Services.AddScoped<IBookRepository, BookRepository>();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
  }

  private static void ConfigureMiddleware(WebApplication app)
  {
    app.UseCors("AllowReactApp");

    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseAuthorization();
    app.MapControllers();
  }
}