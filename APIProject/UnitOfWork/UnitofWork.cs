﻿using APIProject.Models;
using APIProject.Repository;
using Microsoft.EntityFrameworkCore;

namespace APIProject.UnitofWork
{
    public class UnitOfWork
    {
        public readonly SystemMangmentContext _db;
        private GenericRepository<Student> _studentrepo;
        private GenericRepository<Question> _Questionrepo;
        private GenericRepository<Exam> _examrepo;
        private GenericRepository<Student_Exam> _studentexamrepo;
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
        public GenericRepository<Student_Exam> StudentExamRepo
        {
            get
            {
                if( _studentexamrepo == null)
                {
                    _studentexamrepo = new GenericRepository<Student_Exam>(_db);
                }
                return _studentexamrepo;
            }
        }

        public void Save()
        {
           _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
