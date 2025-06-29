using APIProject.DTOs.Exams;
using APIProject.Models;
using APIProject.UnitofWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly UnitOfWork unit;
        private readonly IMapper map;
        public ExamController(UnitOfWork unit, IMapper map)
        {
            this.unit = unit;
            this.map = map;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAllExams()
        {
            List<Exam> exams = unit.ExamRepo.GetAll();
            if (exams == null || exams.Count == 0) return NotFound("No exam found");
            List<DisplayAllExamsDTO> dto = map.Map<List<DisplayAllExamsDTO>>(exams);
            return Ok(dto);
        }
        [HttpGet("searchBy/{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetExamById(int id)
        {
            Exam? exam = unit.ExamRepo.GetById(id);
            if (exam == null) return NotFound($"Exam with ID {id} not found");
            exam.Questions = unit._db.Questions.Where(q => q.Exam_id == id).ToList();  
            DisplayExamsDetailsDTO dto = map.Map<DisplayExamsDetailsDTO>(exam);
            return Ok(dto);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult CreateExam(AddExamDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Exam? exam = map.Map<Exam>(dto);
            unit.ExamRepo.Add(exam);
            unit.Save();
            return Ok(new { message = "Exam created" });

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(40)]
        [ProducesResponseType(404)]
        public IActionResult UpdateExam(int id , EditExamDTO dto)
        {
            Exam? exam = unit.ExamRepo.GetById(id);
            if (exam == null) return NotFound("Exam not found");
            map.Map(dto, exam);
            unit.ExamRepo.Edit(exam ,id);
            unit.Save();
            return Ok(new { message = "Exam updated" });
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult DeleteExam (int id)
        {
            Exam? exam = unit.ExamRepo.GetById(id);
            if (exam == null) return NotFound("Exam not found");
            unit.ExamRepo.Remove(id);
            unit.Save();
            return Ok(new { message = "Exam deleted" });
        }
    }
}
