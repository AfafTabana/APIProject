using APIProject.Models;
using APIProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        GenericRepository<Student> repo;
        public StudentController(GenericRepository<Student> _repo)
        {
            this.repo = _repo;
        }
        #region test feching our project And Generic repo
        [HttpGet]
        public IActionResult showrun()
        {
            return Ok("hello from my app ");
        }
        public IActionResult getallstudents()
        {
            List<Student> sts = repo.GetAll();
            return Ok(sts);
        }
        #endregion




    }
}
