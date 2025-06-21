using APIProject.Models;
using APIProject.Repository;

namespace APIProject.UnitOfWork
{
    public class UnitofWork
    {
        private readonly SystemMangmentContext context;

        private GenericRepository<Student> _studentrepo;
        private GenericRepository<Admin> _adminRepo;
        private GenericRepository<Question> _questionRepo;
        private GenericRepository<Exam> _examRepo;
        private GenericRepository<Student_Exam> _studentExamRepo;
        public UnitofWork(SystemMangmentContext _context)
        {
            this.context = _context;
        }

        public GenericRepository<Student> StudentRepo
        {
            
           get
            {
                if (_studentrepo == null)
                {
                    _studentrepo = new GenericRepository<Student>(_db);
                }
                return _studentrepo;
            }
        }

        public GenericRepository<Admin> AdminRepo
        {
            get
            {
                if (_adminRepo == null)
                    _adminRepo = new GenericRepository<Admin>(context);
                return _adminRepo;
            }
        }
        public GenericRepository<Question> QuestionRepo
        {
            get
            {
                if (_questionRepo == null)
                    _questionRepo = new GenericRepository<Question>(context);
                return _questionRepo;
            }
        }
        public GenericRepository<Exam> ExamRepo
        {
            get
            {
                if (_examRepo == null)
                    _examRepo = new GenericRepository<Exam>(context);
                return _examRepo;
            }
        }
        public GenericRepository<Student_Exam> StudentExamRepo
        {
            get
            {
                if (_studentExamRepo == null)
                    _studentExamRepo = new GenericRepository<Student_Exam>(context);
                return _studentExamRepo;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }


    }
}
