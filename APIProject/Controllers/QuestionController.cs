using APIProject.DTOs.Questions;
using APIProject.DTOs.Students;
using APIProject.Models;
using APIProject.UnitofWork;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {

        private readonly UnitOfWork unitofWork;

        private readonly IMapper mapper;

        public QuestionController(UnitOfWork unit, IMapper map)
        {
            unitofWork = unit;
            mapper = map;
        }

        [HttpPost]
        public ActionResult CreateQuestion(AddQuestionDTO questionDTO)
        {
            if (questionDTO != null)
            {
                Question Question = mapper.Map<Question>(questionDTO);
                unitofWork.Questionrepo.Add(Question);
                unitofWork.Save();
                return Ok(Question);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult EditQuestion(int Id , EditQuestionDTO questionDTO)
        {
            Question EditedQuestion = unitofWork.Questionrepo.GetById(Id);

            if (EditedQuestion == null) { return BadRequest(); }

            if (Id != questionDTO.Id) { return BadRequest();  }
            if (questionDTO == null) { return BadRequest(); }
            else
            {
                Question Question = mapper.Map<Question>(questionDTO);
                Question.Exam_id = EditedQuestion.Exam_id;
                unitofWork.Questionrepo.Edit(Question , Id);
                unitofWork.Save();
                return Ok(Question);
            }

        }

        [HttpDelete]

        public ActionResult DeleteQuestion(int id)
        {
            Question DeletedQuestion = unitofWork.Questionrepo.GetById(id);

            if (DeletedQuestion == null) { 
            
             return BadRequest($"The id : {id} Doesn't Exists");
            }
            if (id < 1)
            {
                return BadRequest();
            }else
            {
                unitofWork.Questionrepo.Remove(id);
                unitofWork.Save();
                return Ok();

            }
                
        }

        [HttpGet]
        public ActionResult GetQuestion(int Exam_id) {
            Exam Exam = unitofWork.ExamRepo.GetById(Exam_id);

            if (Exam == null)
            {

                return BadRequest($"The id : {Exam_id} Doesn't Exists");
            }

            List<Question> Questions =  unitofWork.Questionrepo.GetAll();
            List<DisplayQuestionDTO> QuestionsOfExam = new List<DisplayQuestionDTO>();
            foreach (var Question in Questions) {
                if (Question.Exam_id == Exam_id) {
                    DisplayQuestionDTO Questiondto = mapper.Map<DisplayQuestionDTO>(Question);
                    QuestionsOfExam.Add(Questiondto);
                }
            }

            return Ok (QuestionsOfExam);

        }
    }
}
