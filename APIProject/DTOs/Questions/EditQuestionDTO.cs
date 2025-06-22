namespace APIProject.DTOs.Questions
{
    public class EditQuestionDTO
    {
        public int Id { get; set; }
        public int Grade { get; set; }

        public string Header { get; set; }
        public string Type { get; set; }
        public string Correct_Answer { get; set; }
        public string FWrong_Answer { get; set; }
        public string SWrong_Answer { get; set; }
    }
}
