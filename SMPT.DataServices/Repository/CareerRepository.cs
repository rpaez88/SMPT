using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMPT.DataServices.Data;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;

namespace SMPT.DataServices.Repository
{
    public class CareerRepository : Repository<Career>, ICareerRepository
    {
        public CareerRepository(ILogger logger, AppDbContext context) : base(logger, context) { }


    }
}
