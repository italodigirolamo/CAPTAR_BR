using CAPTAR.Models;
using Microsoft.EntityFrameworkCore;

namespace CAPTAR.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Propietario> Propietario { get; set; }
        public DbSet<Propiedad> Propiedad { get; set; }
        public DbSet<Zona> Zona { get; set; }

        public DbSet<Solicitud> Solicitud { get; set; }
        public DbSet<SoliDetalle> SoliDetalles { get; set; }

        public DbSet<Usuario> Usuario { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Zona>
             (z =>
               {
                   z.ToTable("Zona");
                   z.Property(x => x.id).ValueGeneratedOnAdd().UseIdentityColumn(101, 1);
               });

            modelBuilder.Entity<Zona>()
           .HasData(
                new Zona { id = 101, Nombre = "Parc Central", Descripcion = "Norte de Valencia" },
                new Zona { id = 102, Nombre = "Calicanto - Monte Real", Descripcion = "Playas Norte..." },
                new Zona { id = 103, Nombre = "Zona Avenida al Vedat", Descripcion = "Central..." }
                );

            modelBuilder.Entity<Propietario>()
                .HasMany(p => p.Propiedads)
                .WithOne(pr => pr.Propietario)
                .HasForeignKey(pr => pr.PropietarioId)
                .OnDelete(DeleteBehavior.Cascade)
                ;

            modelBuilder.Entity<Zona>()
                .HasMany(p => p.Propiedads)
                .WithOne(z => z.Zona)
                .HasForeignKey(z => z.ZonaId)
                .OnDelete(DeleteBehavior.Cascade)
                ;
        }

    }
}
