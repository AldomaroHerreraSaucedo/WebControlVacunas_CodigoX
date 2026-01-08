using Microsoft.EntityFrameworkCore;
using WebControlVacunas_CodigoX.Models;

namespace WebControlVacunas_CodigoX.Data
{
    public class ControlVacunasContext: DbContext
    {
        public ControlVacunasContext(DbContextOptions<ControlVacunasContext> options): base(options) { }

        public DbSet<Nino> ninos { get; set; }
        public DbSet<Vacuna> Vacunas { get; set; }
        public DbSet<CronogramaVacunacion> CronogramasVacunacion { get; set; }
        public DbSet<TarjetaControl> TarjetasControl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nino>().ToTable("Nino");
            modelBuilder.Entity<Vacuna>().ToTable("Vacuna");
            modelBuilder.Entity<CronogramaVacunacion>().ToTable("CronogramaVacunacion");
            modelBuilder.Entity<TarjetaControl>().ToTable("TarejetaControl");
            base.OnModelCreating(modelBuilder);
        }
    }
}
