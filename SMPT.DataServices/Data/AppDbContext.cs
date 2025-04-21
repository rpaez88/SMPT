using Microsoft.EntityFrameworkCore;
using SMPT.Entities.DbSet;

namespace SMPT.DataServices.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentState> StudentState { get; set; }
        public virtual DbSet<Evidence> Evidence { get; set; }
        public virtual DbSet<EvidenceState> EvidenceState { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Career> Career { get; set; }
        public virtual DbSet<Cycle> Cycle { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<string>().HaveColumnType("nvarchar(200)");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Career>()
                .HasMany(c => c.Cycles)
                .WithMany(d => d.Careers);

            modelBuilder.Entity<Student>()
                .HasOne(e => e.Career)
                .WithMany(d => d.Students)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Student>()
                .HasOne(e => e.Cycle)
                .WithMany(d => d.Students)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Evidence>()
                .HasOne(a => a.Area)
                .WithMany(a => a.Evidences)
                .HasForeignKey(a => a.AreaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();

            modelBuilder.Entity<Evidence>()
                .HasOne(a => a.Student)
                .WithMany(a => a.Evidences)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired();

            var roleAdminId = Guid.NewGuid();

            modelBuilder.Entity<Role>()
                .HasData(
                    new
                    {
                        Id = roleAdminId,
                        Name = "Administrador",
                        Alias = "admin",
                        Description = "Rol dedicado a la administración de la aplicación.",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new
                    {
                        Id = Guid.NewGuid(),
                        Name = "Coordinador",
                        Alias = "coordinator",
                        Description = "Rol con privilegios de lectura en toda la aplicación.",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new
                    {
                        Id = Guid.NewGuid(),
                        Name = "Responsable de Área",
                        Alias = "area-manager",
                        Description = "Rol con privilegios de lectura y escritura en el área correspondiente de la aplicación.",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new
                    {
                        Id = Guid.NewGuid(),
                        Name = "Estudiante",
                        Alias = "student",
                        Description = "Rol con escritura y lectura en sus datos de evidencias.",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    });

            modelBuilder.Entity<StudentState>()
                .HasData(
                    new
                    {
                        Id = Guid.NewGuid(),
                        Name = "Pasante",
                        Description = "",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new
                    {
                        Id = Guid.NewGuid(),
                        Name = "Egresado",
                        Description = "",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new
                    {
                        Id = Guid.NewGuid(),
                        Name = "Titulado",
                        Description = "",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new
                    {
                        Id = Guid.NewGuid(),
                        Name = "Baja",
                        Description = "",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    });

            modelBuilder.Entity<EvidenceState>()
                .HasData(
                    new
                    {
                        Id = Guid.NewGuid(),
                        Name = "Nueva",
                        Description = "",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new
                    {
                        Id = Guid.NewGuid(),
                        Name = "Aceptada",
                        Description = "",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    },
                    new
                    {
                        Id = Guid.NewGuid(),
                        Name = "Rechazada",
                        Description = "",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    });

            modelBuilder.Entity<User>()
                .HasData(
                    new
                    {
                        Id = Guid.NewGuid(),
                        Code = (long)0,
                        Email = "cuvalles@udg.mx",
                        IsActive = true,
                        Name = "Administrador",
                        Password = "AQAAAAIAAYagAAAAEKWWe3U/k3WqjRYIdoJfV6stmwBxj4PVGKCDJV6ScS3t0OnFaBx/YNtY5/i7+WGXDw==",//SmptCUV@ll35
                        RoleId = roleAdminId,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        Username = "smpt-admin"
                    });
        }
    }
}
