using Microsoft.EntityFrameworkCore;
using SMPT.Entities.DbSet;

namespace SMPT.DataServices.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentState> StudentStates { get; set; }
        public virtual DbSet<Evidence> Evidences { get; set; }
        public virtual DbSet<EvidenceState> EvidenceStates { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Career> Careers { get; set; }
        public virtual DbSet<Cycle> Cycles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Career>()
                .HasMany(c => c.Cycles)
                .WithMany(d => d.Careers);

            modelBuilder.Entity<Career>()
                .HasMany(e => e.Students)
                .WithMany(d => d.Careers);

            modelBuilder.Entity<Evidence>()
                .HasOne(a => a.Area)
                .WithMany(a => a.Evidences)
                .HasForeignKey(a => a.AreaId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            modelBuilder.Entity<Evidence>()
                .HasOne(a => a.Student)
                .WithMany(a => a.Evidences)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
