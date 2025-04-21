using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMPT.DataServices.Data;
using SMPT.DataServices.Repository.Interface;

namespace SMPT.DataServices.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IAreaRepository Areas { get; }
        public ICareerRepository Careers { get; }
        public ICycleRepository Cycles { get; }
        public IEvidenceRepository Evidences { get; }
        public IEvidenceStateRepository EvidenceStates { get; }
        public IRoleRepository Roles { get; }
        public IStudentRepository Students { get; }
        public IStudentStateRepository StudentStates { get; }
        public IUserRepository Users { get; }

        private readonly ILogger _logger;
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("");
            _context = context;
            _repositories = new Dictionary<Type, object>();

            Areas = new AreaRepository(_logger, _context);
            Careers = new CareerRepository(_logger, _context);
            Cycles = new CycleRepository(_logger, _context);
            //Evidences = new EvidenceRepository(_logger, _context);
            //EvidenceStates = new EvidenceStateRepository(_logger, _context);
            Roles = new RoleRepository(_logger, _context);
            Students = new StudentRepository(_logger, _context);
            StudentStates = new StudentStateRepository(_logger, _context);
            Users = new UserRepository(_logger, _context);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepository<T>;
            }

            var repository = new Repository<T>(_logger, _context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
