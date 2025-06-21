using System.ComponentModel.DataAnnotations;

namespace APIProject.Models
{
    public class Exam
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        [DataType(DataType.Date)]
        public DateOnly CreatedAt { get; set; }

        public int Min_grade { get; set; }

        public int Grade { get; set; }

        public virtual List<Student_Exam> Results { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}
