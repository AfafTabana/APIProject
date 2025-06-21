using System.ComponentModel.DataAnnotations.Schema;

namespace APIProject.Models
{
    public class Student_Exam
    {
        public int Id { get; set; }

        public int Student_id { get; set; }

        public int Exam_id { get; set; }

        public int Stud_Grad { get; set; }

        public string Status { get; set; }

        [ForeignKey("Student_id")]
        public Student Student { get; set; }

        [ForeignKey("Exam_id")]
        public Exam Exam { get; set; }
    }
}
