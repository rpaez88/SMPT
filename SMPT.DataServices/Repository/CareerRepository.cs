using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;

namespace SMPT.DataServices.Repository
{
    public class CareerRepository : Repository<Career>, ICareerRepository
    {
        public CareerRepository(ILogger logger, DbContext context) : base(logger, context) { }


    }
}
