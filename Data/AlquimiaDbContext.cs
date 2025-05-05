using Microsoft.EntityFrameworkCore;
using backendAlquimia.Data.Entities;

namespace backendAlquimia.Data
{
    public class AlquimiaDbContext : DbContext
    {
        public AlquimiaDbContext(DbContextOptions<AlquimiaDbContext> options) : base(options)
        {
        }

        // DbSets para cada entidad
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Creador> Creadores { get; set; }
        public DbSet<Combinacion> Combinaciones { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<FamiliaOlfativa> FamiliasOlfativas { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<Intensidad> Intensidades { get; set; }
        public DbSet<CreacionFinal> CreacionesFinales { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<TipoProducto> TiposProducto { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Herencia: Creador y Proveedor extienden Usuario
            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Creador>()
                .HasBaseType<Usuario>()
                .ToTable("Creadores");

            modelBuilder.Entity<Proveedor>()
                .HasBaseType<Usuario>()
                .ToTable("Proveedores");

            // Relación entre CreacionFinal y Creador
            modelBuilder.Entity<CreacionFinal>()
 .HasOne(cf => cf.Creador)
 .WithMany(c => c.HistorialDeCreaciones)
 .HasForeignKey(cf => cf.CreadorId);

            // Configuraciones de Combinacion con Nota
            modelBuilder.Entity<Combinacion>()
                .HasMany(c => c.NotaSalida)
                .WithMany()
                .UsingEntity(j => j.ToTable("CombinacionNotaSalida"));

            modelBuilder.Entity<Combinacion>()
                .HasMany(c => c.NotaCorazon)
                .WithMany()
                .UsingEntity(j => j.ToTable("CombinacionNotaCorazon"));

            modelBuilder.Entity<Combinacion>()
                .HasMany(c => c.NotaFondo)
                .WithMany()
                .UsingEntity(j => j.ToTable("CombinacionNotaFondo"));

            // Ajuste para el nombre de columna en Producto
            modelBuilder.Entity<Producto>()
                .Property(p => p.id)
                .HasColumnName("Id");
        }
    }
}
