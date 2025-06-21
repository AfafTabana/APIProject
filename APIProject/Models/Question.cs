using System.ComponentModel.DataAnnotations.Schema;

namespace APIProject.Models
{
    public class Question
    {
        public int Id { get; set; }

        public int Grade { get; set; }

        public string Header { get; set; }

        public int Exam_id { get; set; }
        public string Type { get; set; } 
        public string Correct_Answer { get; set; } 
        public string FWrong_Answer { get; set; } 
        public string SWrong_Answer { get; set; }

        [ForeignKey("Exam_id")]
        public Exam Exam { get; set; }
    }
}
