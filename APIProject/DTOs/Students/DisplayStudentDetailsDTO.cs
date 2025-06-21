namespace APIProject.DTOs.Students
{
    public class DisplayStudentDetailsDTO
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int NumberOfFailedExams { get; set; }

        public int NumberOfSuccessfulExams { get; set; }
    }
}
