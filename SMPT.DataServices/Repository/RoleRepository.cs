using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMPT.DataServices.Data;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;

namespace SMPT.DataServices.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ILogger logger, AppDbContext context) : base(logger, context)
        {
        }

        public override async Task<IEnumerable<Role>> GetAll()
        {
            try
            {
                return await _dbSet.Where(x => !string.IsNullOrEmpty(x.Name))
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll function", typeof(RoleRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
                if (result == null)
                    return false;

                _dbSet.Remove(result);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function", typeof(RoleRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Role role)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == role.Id);
                if (result == null)
                    return false;

                result.UpdatedDate = DateTime.Now;
                result.Name = role.Name;
                result.Description = role.Description;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function", typeof(RoleRepository));
                throw;
            }
        }
    }
}
