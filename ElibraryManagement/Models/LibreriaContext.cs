using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ElibraryManagement.Models;

public partial class LibreriaContext : DbContext
{
    public LibreriaContext()
    {
    }

    public LibreriaContext(DbContextOptions<LibreriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administradore> Administradores { get; set; }

    public virtual DbSet<Institucione> Instituciones { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<RecuperarContrasena> RecuperarContrasenas { get; set; }

    public virtual DbSet<Suscripcione> Suscripciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=LibreriaDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administradore>(entity =>
        {
            entity.HasKey(e => e.IdAdmin);

            entity.Property(e => e.IdAdmin).ValueGeneratedNever();
            entity.Property(e => e.IdUsuario).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Administradores)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Administradores_Usuarios");
        });

        modelBuilder.Entity<Institucione>(entity =>
        {
            entity.HasKey(e => e.IdInstitucion);

            entity.HasIndex(e => e.IdInstitucion, "Institucion_Unica").IsUnique();

            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro);
            entity.Property(e => e.IdLibro).ValueGeneratedOnAdd();
            entity.Property(e => e.Autor).HasMaxLength(100);
            entity.Property(e => e.Categoria).HasMaxLength(50);
            entity.Property(e => e.IdInstitucion).ValueGeneratedOnAdd();
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.IdInstitucionNavigation)
                .WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdInstitucion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Libros_Instituciones");
        });

        modelBuilder.Entity<RecuperarContrasena>(entity =>
        {
            entity.HasKey(e => e.IdRecuperacion);

            entity.ToTable("Recuperar_Contrasena");

            entity.Property(e => e.IdRecuperacion).ValueGeneratedNever();
            entity.Property(e => e.CodigoRecuperacion).HasMaxLength(10);
            entity.Property(e => e.FechaSolicitud)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdUsuario).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.RecuperarContrasenas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recuperar_Contrasena_Usuarios");
        });

        modelBuilder.Entity<Suscripcione>(entity =>
        {
            entity.HasKey(e => e.IdSuscripcion);

            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.FechaSuscripcion)
                .HasMaxLength(10)
                .HasDefaultValueSql("(getdate())")
                .IsFixedLength();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.HasIndex(e => e.IdUsuario, "UQ_Usuarios_CorreoElectronico").IsUnique();

            entity.Property(e => e.Contrasena).HasMaxLength(150);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);

            entity.HasOne(d => d.IdInstitucionNavigation)
        .WithMany(p => p.Usuarios)
        .HasForeignKey(d => d.IdInstitucion)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Usuarios_Instituciones");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
