using APIProject.DTOs.Students;
using APIProject.Models;
using AutoMapper;

namespace APIProject.Mapper
{
    public class StudentMapper :Profile
    {
        public StudentMapper()
        {
            CreateMap<Student, DisplayStudentDTO>().ReverseMap();
            


        }
    }
}
