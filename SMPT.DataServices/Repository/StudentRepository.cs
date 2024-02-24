using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;

namespace SMPT.DataServices.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ILogger logger, DbContext context) : base(logger, context) { }

        public override async Task<IEnumerable<Student>> GetAll()
        {
            try
            {
                return await _dbSet.Where(a => !string.IsNullOrEmpty(a.Name))
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(a => a.Name)
                    .Include(u => u.Role)
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
                .Include(u => u.Role)
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
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
                if (result == null)
                    return false;

                result.UpdatedDate = DateTime.Now;
                result.Name = entity.Name;
                result.Code = entity.Code;
                result.Email = entity.Email;
                result.RoleId = entity.RoleId;
                result.IsActive = entity.IsActive;
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
