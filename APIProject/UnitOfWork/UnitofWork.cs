using APIProject.Models;
using APIProject.Repository;

namespace APIProject.UnitofWork
{
    public class UnitOfWork
    {
        public readonly SystemMangmentContext _db;
        private GenericRepository<Student> _studentrepo;
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

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
