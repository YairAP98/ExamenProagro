using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class ExamenProagroContext : DbContext
{
    public ExamenProagroContext()
    {
    }

    public ExamenProagroContext(DbContextOptions<ExamenProagroContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Georeferencia> Georeferencias { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Prueba> Pruebas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= ExamenProagro; TrustServerCertificate=True; Trusted_Connection=True;User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC1E18D3FD3");

            entity.ToTable("Estado");

            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Siglas)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Georeferencia>(entity =>
        {
            entity.HasKey(e => e.IdGeoreferencia).HasName("PK__Georefer__5D9CC3A35DD0A10D");

            entity.Property(e => e.Latitud).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitud).HasColumnType("decimal(9, 6)");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Georeferencia)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Georefere__IdEst__20C1E124");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.IdEstadoNavigation).WithMany()
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Permisos__IdEsta__239E4DCF");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Permisos__IdUsua__22AA2996");
        });

        modelBuilder.Entity<Prueba>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Prueba");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany()
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Prueba__IdEstado__4316F928");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Prueba__IdUsuari__4222D4EF");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97F0CF4B74");

            entity.ToTable("Usuario");

            entity.Property(e => e.Contrasena)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Creacion).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rfc)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("RFC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
