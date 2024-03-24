using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Data;
using Server.Data.Implementations;
using Server.Data.Interfaces;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<DataContext>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
        builder.Services.AddScoped<ITodoRepository, TodoRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        // builder.Services.AddTransient<ITodoRepository, TodoRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        // app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.MapControllers();

        app.Run();
    }
}