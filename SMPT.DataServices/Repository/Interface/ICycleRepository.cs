using SMPT.Entities.DbSet;

namespace SMPT.DataServices.Repository.Interface
{
    public interface ICycleRepository : IRepository<Cycle>
    {
        Task<IEnumerable<Guid>?> GetCareerIds(Guid cycleId);
    }
}
