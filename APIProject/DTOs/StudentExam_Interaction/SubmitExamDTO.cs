namespace APIProject.DTOs.StudentExam_Interaction
{
    public class SubmitExamDTO
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
