namespace APIProject.Models
{
    public class Student
    {

        public int Id { get; set; }

        public string Email {get; set; }

        public string Username { get; set; }

       

       public virtual List<Student_Exam> Results { get; set; }

    }
}
