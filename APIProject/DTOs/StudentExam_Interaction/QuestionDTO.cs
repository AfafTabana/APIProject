namespace APIProject.DTOs.StudentExam_Interaction
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Type { get; set; }
        public List<string> Choices { get; set; }
    }
}
