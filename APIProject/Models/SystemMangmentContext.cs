using Microsoft.EntityFrameworkCore;
using System;

namespace APIProject.Models
{
    public class SystemMangmentContext :DbContext
    {
        public SystemMangmentContext()
        {

        }
        public SystemMangmentContext(DbContextOptions<SystemMangmentContext> options)
       : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Admin> Admins { get; set; }    

        public DbSet<Student_Exam> Student_Exams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
