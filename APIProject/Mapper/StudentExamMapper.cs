using APIProject.DTOs.StudentExam_Interaction;
using APIProject.Models;
using AutoMapper;

namespace APIProject.Mapper
{
    public class StudentExamMapper :Profile
    {
        public StudentExamMapper()
        {
            CreateMap<Exam, ExamForStudentDTO>();
        }
    }
}
