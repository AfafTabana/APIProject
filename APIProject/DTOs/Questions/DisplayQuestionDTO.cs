namespace APIProject.DTOs.Questions
{
    public class DisplayQuestionDTO
    {
        public string Header { get; set; }

        public string Correct_Answer { get; set; }
        public string FWrong_Answer { get; set; }
        public string SWrong_Answer { get; set; }
        public int Grade { get; set; }
    }
}
