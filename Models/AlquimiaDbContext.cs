using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using backendAlquimia.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace backendAlquimia.Models
{
    public class AlquimiaDbContext : DbContext
    {
        public AlquimiaDbContext(DbContextOptions<AlquimiaDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Creador> Creadores { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<TipoProducto> TiposProducto { get; set; }

        public DbSet<Formula> Formulas { get; set; }
        public DbSet<CreacionFinal> CreacionesFinales { get; set; }

        public DbSet<Intensidad> Intensidades { get; set; }
        public DbSet<Combinacion> Combinaciones { get; set; }

        public DbSet<Nota> Notas { get; set; }
        public DbSet<FamiliaOlfativa> FamiliasOlfativas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Clave primaria para Combinacion
            modelBuilder.Entity<Combinacion>().HasKey(c => c.Id);

            // Relaciones entre Nota y Familia
            modelBuilder.Entity<Nota>()
                .HasOne(n => n.FamiliaOlfativa)
                .WithMany(f => f.Notas)
                .HasForeignKey(n => n.FamiliaOlfativaId);

            // Configuración para herencia TPH (Usuario -> Creador, Proveedor)
            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Creador>("Creador")
                .HasValue<Proveedor>("Proveedor");

            // Posible corrección para relación entre Formula y Combinacion/Intensidad
            modelBuilder.Entity<Formula>()
                .HasOne(f => f.Intensidad)
                .WithMany()
                .HasForeignKey("IntensidadId");

            modelBuilder.Entity<Formula>()
                .HasOne(f => f.Combinacion)
                .WithMany()
                .HasForeignKey("CombinacionId");

            // Relación muchos a muchos con Combinacion y Notas
            modelBuilder.Entity<Combinacion>()
                .HasMany(c => c.NotaSalida)
                .WithMany()
                .UsingEntity(j => j.ToTable("NotasSalida"));

            modelBuilder.Entity<Combinacion>()
                .HasMany(c => c.NotaCorazon)
                .WithMany()
                .UsingEntity(j => j.ToTable("NotasCorazon"));

            modelBuilder.Entity<Combinacion>()
                .HasMany(c => c.NotaFondo)
                .WithMany()
                .UsingEntity(j => j.ToTable("NotasFondo"));

            // Clave primaria para CreacionFinal (la tenés que agregar)
            modelBuilder.Entity<CreacionFinal>()
                .HasKey(cf => cf.Id);
        }
    }

}
