using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace alquimia.Data.Data.Entities;

public partial class AlquimiaDbContext : DbContext
{
    public AlquimiaDbContext()
    {
    }

    public AlquimiaDbContext(DbContextOptions<AlquimiaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<CompatibilidadesFamilia> CompatibilidadesFamilias { get; set; }

    public virtual DbSet<Design> Designs { get; set; }

    public virtual DbSet<EntidadFinal> EntidadFinals { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<FamiliasOlfativa> FamiliasOlfativas { get; set; }

    public virtual DbSet<Formula> Formulas { get; set; }

    public virtual DbSet<FormulaNotum> FormulaNota { get; set; }

    public virtual DbSet<Intensidade> Intensidades { get; set; }

    public virtual DbSet<Nota> Notas { get; set; }

    public virtual DbSet<NotasPiramideFamilium> NotasPiramideFamilia { get; set; }

    public virtual DbSet<Opcione> Opciones { get; set; }

    public virtual DbSet<OpinionUsuarioProducto> OpinionUsuarioProductos { get; set; }

    public virtual DbSet<OpinionUsuarioProveedor> OpinionUsuarioProveedors { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoProducto> PedidoProductos { get; set; }

    public virtual DbSet<PiramideOlfativa> PiramideOlfativas { get; set; }

    public virtual DbSet<Pregunta> Preguntas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Suscripcion> Suscripcions { get; set; }

    public virtual DbSet<TipoProducto> TipoProductos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioProducto> UsuarioProductos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=AXEL\\SQLEXPRESS;Database=alquimiaDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PK_UserRoles");
        });

        modelBuilder.Entity<CompatibilidadesFamilia>(entity =>
        {
            entity.HasOne(d => d.Familia1).WithMany(p => p.CompatibilidadesFamiliaFamilia1s).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Familia2).WithMany(p => p.CompatibilidadesFamiliaFamilia2s).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Design>(entity =>
        {
            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Designs).HasConstraintName("FK_design_productoId");

            entity.HasOne(d => d.TipoProducto).WithMany(p => p.Designs).HasConstraintName("FK_design_tipoProducto");
        });

        modelBuilder.Entity<EntidadFinal>(entity =>
        {
            entity.HasOne(d => d.Design).WithMany(p => p.EntidadFinals).HasConstraintName("FK_EntidadFinal_Design");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.EntidadFinals).HasConstraintName("FK_EntidadFinal_IdUsuario");

            entity.HasOne(d => d.Productos).WithMany(p => p.EntidadFinals).HasConstraintName("FK_EntidadFinal_ProductoId");
        });

        modelBuilder.Entity<Formula>(entity =>
        {
            entity.HasOne(d => d.FormulaCorazonNavigation).WithMany(p => p.FormulaFormulaCorazonNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Formulas_corazon");

            entity.HasOne(d => d.FormulaFondoNavigation).WithMany(p => p.FormulaFormulaFondoNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Formulas_fondo");

            entity.HasOne(d => d.FormulaSalidaNavigation).WithMany(p => p.FormulaFormulaSalidaNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Formulas_salida");

            entity.HasOne(d => d.Intensidad).WithMany(p => p.Formulas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Formulas_intensidad");
        });

        modelBuilder.Entity<FormulaNotum>(entity =>
        {
            entity.Property(e => e.FormulaNotaId).ValueGeneratedNever();

            entity.HasOne(d => d.NotaId1Navigation).WithMany(p => p.FormulaNotumNotaId1Navigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_nota1");

            entity.HasOne(d => d.NotaId2Navigation).WithMany(p => p.FormulaNotumNotaId2Navigations).HasConstraintName("FK_nota2");

            entity.HasOne(d => d.NotaId3Navigation).WithMany(p => p.FormulaNotumNotaId3Navigations).HasConstraintName("FK_nota3");

            entity.HasOne(d => d.NotaId4Navigation).WithMany(p => p.FormulaNotumNotaId4Navigations).HasConstraintName("FK_nota4");

            entity.HasOne(d => d.PiramideOlfativa).WithMany(p => p.FormulaNota).HasConstraintName("FK_piramideOlfativa");
        });

        modelBuilder.Entity<Intensidade>(entity =>
        {
            entity.Property(e => e.Nombre).HasDefaultValue("");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasOne(d => d.PiramideOlfativa).WithMany(p => p.Nota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_notasPiramideOlfativa");

            entity.HasMany(d => d.NotaIncompatibles).WithMany(p => p.NotaNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "NotasIncompatible",
                    r => r.HasOne<Nota>().WithMany()
                        .HasForeignKey("NotaIncompatibleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_NotaIncompatible_notaIncompatible"),
                    l => l.HasOne<Nota>().WithMany()
                        .HasForeignKey("NotaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_NotaIncompatible_nota"),
                    j =>
                    {
                        j.HasKey("NotaId", "NotaIncompatibleId").HasName("PK__NotasInc__06842A27ECDF918A");
                        j.ToTable("NotasIncompatibles");
                    });

            entity.HasMany(d => d.NotaNavigation).WithMany(p => p.NotaIncompatibles)
                .UsingEntity<Dictionary<string, object>>(
                    "NotasIncompatible",
                    r => r.HasOne<Nota>().WithMany()
                        .HasForeignKey("NotaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_NotaIncompatible_nota"),
                    l => l.HasOne<Nota>().WithMany()
                        .HasForeignKey("NotaIncompatibleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_NotaIncompatible_notaIncompatible"),
                    j =>
                    {
                        j.HasKey("NotaId", "NotaIncompatibleId").HasName("PK__NotasInc__06842A27ECDF918A");
                        j.ToTable("NotasIncompatibles");
                    });
        });

        modelBuilder.Entity<NotasPiramideFamilium>(entity =>
        {
            entity.ToView("NotasPiramideFamilia");
        });

        modelBuilder.Entity<OpinionUsuarioProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OpinionProducto");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.OpinionUsuarioProductos).HasConstraintName("FK_opinion_ProductoId");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.OpinionUsuarioProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_opinionUsuario_producto");
        });

        modelBuilder.Entity<OpinionUsuarioProveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Opinion");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.OpinionUsuarioProveedorIdProveedorNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_opinionProveedor");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.OpinionUsuarioProveedorIdUsuarioNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_opinionUsuario");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Pedidos");

            entity.HasOne(d => d.Estado).WithMany(p => p.InverseEstado).HasConstraintName("FK_pedidosEstado");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pedidos).HasConstraintName("FK_pedidosUsuario");
        });

        modelBuilder.Entity<PedidoProducto>(entity =>
        {
            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidoProductos).HasConstraintName("FK_pedidoProductoIdPedido");

            entity.HasOne(d => d.Productos).WithMany(p => p.PedidoProductos).HasConstraintName("FK_pedidoProductoId");
        });

        modelBuilder.Entity<Pregunta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Pregunta");

            entity.HasOne(d => d.IdOpcionesNavigation).WithMany(p => p.Pregunta).HasConstraintName("FK_PreguntasOpciones");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProductoIdProveedorNavigations).HasConstraintName("FK_productoIdProv");

            entity.HasOne(d => d.TipoProducto).WithMany(p => p.Productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tipoProducto");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ProductoUsuarios).HasConstraintName("FK_productoIdUser");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasOne(d => d.IdPreguntaNavigation).WithMany(p => p.Quizzes).HasConstraintName("FK_quizPreguntas");
        });

        modelBuilder.Entity<Suscripcion>(entity =>
        {
            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Suscripcions).HasConstraintName("FK_estado_suscripcion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Usuarios).HasConstraintName("FK_estado_usuario");

            entity.HasOne(d => d.IdFormulasNavigation).WithMany(p => p.Usuarios).HasConstraintName("FK_formulas_usuario");

            entity.HasOne(d => d.IdQuizNavigation).WithMany(p => p.Usuarios).HasConstraintName("FK_quiz_usuario");

            entity.HasOne(d => d.IdSuscripcionNavigation).WithMany(p => p.Usuarios).HasConstraintName("FK_suscripcion_usuario");
        });

        modelBuilder.Entity<UsuarioProducto>(entity =>
        {
            entity.HasOne(d => d.Producto).WithMany(p => p.UsuarioProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuarioProductos_producto");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuarioProductos_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
