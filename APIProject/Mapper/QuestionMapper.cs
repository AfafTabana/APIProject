using APIProject.Models;
using AutoMapper;
using APIProject.DTOs.Questions;

namespace APIProject.Mapper
{
    public class QuestionMapper : Profile
    {

        public QuestionMapper() {

            CreateMap<Question, AddQuestionDTO>().ReverseMap();

            CreateMap<Question, EditQuestionDTO>().ReverseMap();

            CreateMap<Question, DisplayQuestionDTO>().ReverseMap();

        }
       
    }
}
