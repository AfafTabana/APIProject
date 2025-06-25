using APIProject.DTOs.Exams;
using APIProject.DTOs.Questions;
using APIProject.Models;
using AutoMapper;

namespace APIProject.Mapper
{
    public class ExamMapper: Profile
    {
        public  ExamMapper()
        {
            CreateMap<Exam, DisplayExamsDetailsDTO>();
            CreateMap<Exam, DisplayAllExamsDTO>();
            CreateMap<AddExamDTO, Exam>().ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.Now)));
            CreateMap<EditExamDTO, Exam>();
            CreateMap<Question, DisplayQuestionDTO>().ReverseMap();
        }
    }
}
