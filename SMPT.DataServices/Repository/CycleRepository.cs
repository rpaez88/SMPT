using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMPT.DataServices.Data;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;

namespace SMPT.DataServices.Repository
{
    public class CycleRepository : Repository<Cycle>, ICycleRepository
    {
        public CycleRepository(ILogger logger, AppDbContext context) : base(logger, context) { }

        public async Task<IEnumerable<Guid>?> GetCareerIds(Guid cycleId)
        {
            var cycleWithCareersIds = await _dbSet.Select(c =>  new { c.Id, Careers = c.Careers.Select(x => x.Id).ToList() })
                .FirstOrDefaultAsync(x => x.Id == cycleId);

            return cycleWithCareersIds?.Careers;
        }
    }
}
