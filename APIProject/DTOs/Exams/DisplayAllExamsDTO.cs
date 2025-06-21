namespace APIProject.DTOs.Exams
{
    public class DisplayAllExamsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public DateOnly CreatedAt { get; set; }

        public int Min_grade { get; set; }

        public int Grade { get; set; }
    }
}
