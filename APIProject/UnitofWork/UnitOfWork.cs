using APIProject.Models;
using APIProject.Repository;

namespace APIProject.UnitofWork
{
    public class UnitOfWork
    {
        public readonly SystemMangmentContext _db;
        private GenericRepository<Student> _studentrepo;
        public UnitOfWork( SystemMangmentContext db)
        private GenericRepository<Question> _Questionrepo;
        private GenericRepository<Exam> _examrepo;
        public UnitOfWork( SystemMangmentContext db)
        {
            this._db = db;
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

        public GenericRepository<Question> Questionrepo
        {
            get
            {
                if (_Questionrepo == null) {

                    _Questionrepo = new GenericRepository<Question>(_db);


                }

                return _Questionrepo;

            }
        }

        public GenericRepository<Exam> ExamRepo
        {
            get
            {
                if (_examrepo == null)
                {
                    _examrepo = new GenericRepository<Exam>(_db);
                }
                return _examrepo;
            }
        }

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
           _db.SaveChanges();
        }
    }
}
