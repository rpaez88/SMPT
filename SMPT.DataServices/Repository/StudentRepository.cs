using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMPT.DataServices.Data;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;

namespace SMPT.DataServices.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ILogger logger, AppDbContext context) : base(logger, context) { }

        public override async Task<IEnumerable<Student>> GetAll()
        {
            try
            {
                return await _dbSet.Where(a => !string.IsNullOrEmpty(a.Name))
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(a => a.Name)
                    .Include(u => u.User)
                        .ThenInclude(r => r.Role)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll function", typeof(StudentRepository));
                throw;
            }
        }

        public override async Task<Student?> GetById(Guid id)
        {
            return await _dbSet.Where(u => u.Id == id)
                .AsNoTracking()
                .AsSplitQuery()
                .Include(u => u.User)
                    .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync();
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(a => a.Id == id);
                if (result == null)
                    return false;

                _dbSet.Remove(result);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function", typeof(StudentRepository));
                throw;
            }
        }

        public override async Task<bool> Add(Student entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            return await base.Add(entity);
        }

        public override async Task<bool> Update(Student entity)
        {
            try
            {
                var result = await GetById(entity.Id);
                if (result == null)
                    return false;

                result.UpdatedDate = DateTime.Now;
                result.Name = entity.Name;
                result.User.Name = entity.Name;
                result.User.Code = entity.User.Code;
                result.User.Email = entity.User.Email;
                result.User.RoleId = entity.User.RoleId;
                result.User.IsActive = entity.User.IsActive;
                result.User.UpdatedDate = DateTime.Now;
                result.StateId = entity.StateId;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function", typeof(StudentRepository));
                throw;
            }
        }
    }
}
