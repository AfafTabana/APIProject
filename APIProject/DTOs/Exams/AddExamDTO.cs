using APIProject.DTOs.Questions;
using APIProject.Models;

namespace APIProject.DTOs.Exams
{
    public class AddExamDTO
    {

        public string Name { get; set; }

        public string Category { get; set; }

        public DateOnly CreatedAt { get; set; }

        public int Min_grade { get; set; }

        public int Grade { get; set; }

        public List<DisplayQuestionDTO> Questions { get; set; }
    }
}
