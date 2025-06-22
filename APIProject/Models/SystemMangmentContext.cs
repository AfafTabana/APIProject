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
        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Exam> Exams { get; set; }

        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }    

        public virtual DbSet<Student_Exam> Student_Exams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
