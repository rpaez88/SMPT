using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;

namespace SMPT.DataServices.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ILogger logger, DbContext context) : base(logger, context)
        {
        }

        public override async Task<IEnumerable<User>> GetAll()
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
                _logger.LogError(ex, "{Repo} GetAll function", typeof(UserRepository));
                throw;
            }
        }

        public override async Task<User?> GetById(Guid id)
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
                _logger.LogError(ex, "{Repo} Delete function", typeof(UserRepository));
                throw;
            }
        }
    }
}
