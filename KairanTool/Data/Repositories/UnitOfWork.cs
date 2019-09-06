using System;
using KairanTool.Data.Interfaces;
using KairanTool.Models;

namespace KairanTool.Data.Repositories
{
    public class UnitOfWork :IDisposable,IUnitOfWork
    {
        private readonly KairanToolDbContext _context;

        public UnitOfWork(KairanToolDbContext context)
        {
            _context = context;
        }

        private Repository<Kairan> _kairanRepository;
        private Repository<Role> _roleRepository;
//        private Repository<Subject> _subjectRepository;
//        private Repository<Role> _roleRepository;
//        private Repository<Semester> _semesterRepository;
//        private Repository<Transcript> _transcriptRepository;
//        private Repository<Schedule> _scheduleRepository;
        
        public IRepository<Kairan> KairanRepository => _kairanRepository ?? (_kairanRepository = new Repository<Kairan>(_context));
        public IRepository<Role> RoleRepository => _roleRepository ?? (_roleRepository = new Repository<Role>(_context));
//        public IRepository<Subject> SubjectRepository => _subjectRepository ?? (_subjectRepository = new Repository<Subject>(_context));
//        public IRepository<Role> RoleRepository => _roleRepository ?? (_roleRepository = new Repository<Role>(_context));
//        public IRepository<Semester> SemesterRepository => _semesterRepository ?? (_semesterRepository = new Repository<Semester>(_context));
//        public IRepository<Transcript> TranscriptRepository => _transcriptRepository ?? (_transcriptRepository = new Repository<Transcript>(_context));
//        public IRepository<Schedule> ScheduleRepository => _scheduleRepository ?? (_scheduleRepository = new Repository<Schedule>(_context));
//        
        
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool _disposed ;
 
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}