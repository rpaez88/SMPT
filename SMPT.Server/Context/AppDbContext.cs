using Microsoft.EntityFrameworkCore;
using SMPT.Server.Models;

namespace SMPT.Server.Context
{
    public class AppDbContext: DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<StudentState> StudentStates { get; set; }
        DbSet<Evidence> Evidences { get; set; }
        DbSet<EvidenceState> EvidenceStates { get; set; }
        DbSet<Area> Areas { get; set; }
        DbSet<AreaResponsible> AreaResponsibles { get; set; }
        DbSet<Career> Careers { get; set; }
        DbSet<Cycle> Cycles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
    }
}
