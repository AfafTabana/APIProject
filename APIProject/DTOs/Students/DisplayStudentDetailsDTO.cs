using System.ComponentModel.DataAnnotations;

namespace APIProject.DTOs.Students
{
    public class DisplayStudentDetailsDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
         public string Username { get; set; }
        public int NumberOfFailedExams { get; set; }
        public int NumberOfSuccessfulExams { get; set; }

        
        public List<StudentExamDto> StudentExams { get; set; } = new List<StudentExamDto>();





    }
}
