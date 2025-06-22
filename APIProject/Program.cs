
using APIProject.Mapper;
using APIProject.Models;
using APIProject.Repository;
using APIProject.UnitofWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using APIProject.Mapper;

namespace APIProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Regestir connection string 
            builder.Services.AddDbContext<SystemMangmentContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(builder.Configuration.GetConnectionString("db"));
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            #region Register services Here 
            //1=== register generic repository
            builder.Services.AddScoped<GenericRepository<Student>>();
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(StudentMapper));
            builder.Services.AddScoped<GenericRepository<Question>>();
            builder.Services.AddScoped<GenericRepository<Exam>>();

            #endregion
            builder.Services.AddAutoMapper(typeof(QuestionMapper));
            builder.Services.AddScoped<UnitOfWork>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
