using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taller.API.Datos.Entidades;

namespace Taller.API.Datos
{
    public class DataContext: IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<DocumentType> DocumentTypes { get; set; }

        public DbSet<Procedure> Procedures { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DocumentType>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<VehicleType>().HasIndex(x => x.Description).IsUnique();
            modelBuilder.Entity<Procedure>().HasIndex(x => x.Description).IsUnique();
        }
    }
}
