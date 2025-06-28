
using APIProject.Mapper;
using APIProject.Models;
using APIProject.Repository;
using APIProject.UnitofWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Identity.Client;
using System;
using System.Text;
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

            #region Cors 
            // Add CORS service
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    policy => policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            });

            #endregion

            #region Register services Here 
            //1=== register generic repository
            builder.Services.AddScoped<GenericRepository<Student>>();
            builder.Services.AddScoped<GenericRepository<Exam>>();
            builder.Services.AddScoped<UnitOfWork>();
              builder.Services.AddAutoMapper(typeof(StudentMapper));
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(StudentMapper));
            builder.Services.AddScoped<GenericRepository<Question>>();
            builder.Services.AddScoped<GenericRepository<Exam>>();

            //=== register Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
            options =>
            {
               options.Password.RequireDigit = true;
               options.Password.RequiredLength = 6;
               options.Password.RequireLowercase = false;
               options.Password.RequireUppercase = false;
               options.Password.RequireNonAlphanumeric = false;

            }
            )
           .AddEntityFrameworkStores<SystemMangmentContext>();

            #endregion
            #region  jwt validation
            builder.Services.AddAuthentication(op => op.DefaultAuthenticateScheme = "myschema")
                .AddJwtBearer("myschema", option => {

                    var key = "Welcome to our system Exam this task by ahmed ashraf";
                    var secretkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = secretkey,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };


                });


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

            app.UseCors("AllowAngularApp");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
