namespace APIProject.DTOs.StudentExam_Interaction
{
    public class StudentWithResultsDTO
    {
        public string StudentName { get; set; }
        public List<StudentResultDTO> Results { get; set; }
    }
}
