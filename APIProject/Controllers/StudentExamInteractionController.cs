using APIProject.DTOs.StudentExam_Interaction;
using APIProject.Models;
using APIProject.UnitofWork;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExamInteractionController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly IMapper map;

        public StudentExamInteractionController(UnitOfWork unit, IMapper map)
        {
            this.unit = unit;
            this.map = map;
        }

        [HttpGet("available-exams")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllAvailableExams()
        {
            var exams = unit.ExamRepo.GetAll();
            if (exams == null || exams.Count == 0) return NotFound("No available exams found");

            var dto = map.Map<List<ExamForStudentDTO>>(exams);
            return Ok(dto);
        }

        [HttpGet("take-exam/{examId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetExamToTake(int examId)
        {
            var exam = unit.ExamRepo.GetById(examId);
            if (exam == null) return NotFound("Exam not found");

            exam.Questions = unit._db.Questions.Where(q => q.Exam_id == examId).ToList();

            var dto = new TakeExamDTO
            {
                Id = exam.Id,
                Name = exam.Name,
                Questions = exam.Questions.Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    Header = q.Header,
                    Type = q.Type,
                    Choices = new List<string> { q.Correct_Answer, q.FWrong_Answer, q.SWrong_Answer } 
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost("submit-exam")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult SubmitExam(SubmitExamDTO dto)
        {
            var exam = unit.ExamRepo.GetById(dto.ExamId);
            if (exam == null) return NotFound("Exam not found");

            exam.Questions = unit._db.Questions.Where(q => q.Exam_id == dto.ExamId).ToList();

            int correct = 0;
            foreach (var ans in dto.Answers)
            {
                var q = exam.Questions.FirstOrDefault(q => q.Id == ans.QuestionId);
                if (q != null && q.Correct_Answer == ans.Answer)
                    correct += q.Grade;
            }

            var studentExam = new Student_Exam
            {
                Student_id = dto.StudentId,
                Exam_id = dto.ExamId,
                Stud_Grad = correct,
                Status = "Submitted"
            };

            unit.StudentExamRepo.Add(studentExam);
            unit.Save();

            return Ok(new { message = "Exam submitted successfully", grade = correct });
        }

        [HttpGet("student-results/{studentId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetStudentResults(int studentId)
        {
            var results = unit._db.Student_Exams
                .Where(s => s.Student_id == studentId)
                .ToList();

            if (!results.Any()) return NotFound("No results found");

            var dto = results.Select(r => new StudentResultDTO
            {
                ExamName = unit.ExamRepo.GetById(r.Exam_id)?.Name ?? "Unknown",
                Grade = r.Stud_Grad
            }).ToList();

            return Ok(dto);
        }

        [HttpGet("admin/all-results")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllStudentResults()
        {
            var results = unit._db.Student_Exams.ToList();
            if (!results.Any()) return NotFound("No student results");
            List<StudentWithResultsDTO> grouped = results.GroupBy(r => r.Student_id)
                .Select(g => new StudentWithResultsDTO
                {
                    StudentName = unit.StudentRepo.GetById(g.Key)?.Username ?? "unknown",
                    Results = g.Select(r => new StudentResultDTO
                    {
                        ExamName = unit.ExamRepo.GetById(r.Exam_id).Name ?? "unknown",
                        Grade = r.Stud_Grad
                    }).ToList()
                }).ToList();
            return Ok(grouped);
                
                }
    }
    }
