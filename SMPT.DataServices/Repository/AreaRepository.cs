using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMPT.DataServices.Data;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;

namespace SMPT.DataServices.Repository
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(ILogger logger, AppDbContext context) : base(logger, context)
        {
        }

        public override async Task<IEnumerable<Area>> GetAll()
        {
            try
            {
                return await _dbSet.Where(a => !string.IsNullOrEmpty(a.Name))
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(a => a.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll function", typeof(AreaRepository));
                throw;
            }
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
                _logger.LogError(ex, "{Repo} Delete function", typeof(AreaRepository));
                throw;
            }
        }
    }
}
