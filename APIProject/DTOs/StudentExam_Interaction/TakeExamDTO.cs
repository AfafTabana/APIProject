namespace APIProject.DTOs.StudentExam_Interaction
{
    public class TakeExamDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuestionDTO> Questions { get; set; }
    }
}
