﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIProject.Models
{
    public class Student
    {

        public int Id { get; set; }

        public string Email {get; set; }

        public string Username { get; set; }

        public virtual List<Student_Exam> Results { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

     

    }
}
