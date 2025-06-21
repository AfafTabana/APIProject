namespace APIProject.Models
{
    public class Student
    {

        public int Id { get; set; }

        public string Email {get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

       public List<Student_Exam> Results { get; set; }

    }
}
