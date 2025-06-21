using APIProject.Models;
using APIProject.Repository;
using APIProject.UnitofWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIProject.DTOs.Students;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly IMapper map;

        public StudentController(UnitOfWork unit, IMapper map)
        {
            this.unit = unit;
            this.map = map;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
          public IActionResult getallstudents()
        {
            List<Student> sts = unit.StudentRepo.GetAll();
            
            if (sts == null || sts.Count == 0)
            {
                return NotFound("No students found.");
            }
            List<DisplayStudentDTO> studentDtos = map.Map< List<DisplayStudentDTO>>(sts);
            return Ok(studentDtos);
        }

        [HttpGet("searchBy/{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult getstudentbyid(int id)
        {
            Student? student = unit.StudentRepo.GetById(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            DisplayStudentDTO studentDto = map.Map<DisplayStudentDTO>(student);
            return Ok(studentDto);
        }

        [HttpGet("searchBy/{username:alpha}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult getstudentbyusername(string username)
        {
            var students = unit.StudentRepo.GetAll().Where(s => s.Username.Contains(username));
            if (students == null)
            {
                return NotFound($"Student with username {username} not found.");
            }
           List< DisplayStudentDTO >studentDto = map.Map<List<DisplayStudentDTO>>(students);
            return Ok(studentDto);
        }


        [HttpGet("details/{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult getstudentdetailsbyid(int id)
        {
            Student? student = unit.StudentRepo.GetById(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            int numOfFailedExams = unit._db.Student_Exams
                .Where(result=>result.Student_id==id && result.Stud_Grad<result.Exam.Min_grade)
                .Count();
            int numOfPassedExams = unit._db.Student_Exams
                .Where(result => result.Student_id == id && result.Stud_Grad >= result.Exam.Min_grade)
                .Count();

            List<StudentExamDto> exams =unit._db.Student_Exams
                .Where(result => result.Student_id == id)
                .Select(result => new StudentExamDto
                {
                   Name = result.Exam.Name,
                    Category = result.Exam.Category,
                    StudentGrade = result.Stud_Grad,
                    FullMark = result.Exam.Grade
                }).ToList();

            DisplayStudentDetailsDTO std = new DisplayStudentDetailsDTO();
            std.Id = student.Id;
            std.Username = student.Username;
            std.Email = student.Email;
            std.NumberOfFailedExams = numOfFailedExams;
            std.NumberOfSuccessfulExams = numOfPassedExams;
            std.StudentExams = exams;

            return Ok(std);

        }









    }
}
