using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace prototipoCantina.Models
{
    public partial class cantinabbddContext : DbContext
    {
        public cantinabbddContext()
        {
        }

        public cantinabbddContext(DbContextOptions<cantinabbddContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Credito> Creditos { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Pago> Pagos { get; set; } = null!;
        public virtual DbSet<Reserva> Reservas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Credito>(entity =>
            {
                entity.HasKey(e => e.IdCredito)
                    .HasName("PRIMARY");

                entity.ToTable("creditos");

                entity.HasIndex(e => e.IdUsuario, "id_usuario");

                entity.Property(e => e.IdCredito).HasColumnName("id_credito");

                entity.Property(e => e.CreditoConsumido)
                    .HasPrecision(6, 2)
                    .HasColumnName("credito_consumido");

                entity.Property(e => e.CreditoDiario)
                    .HasPrecision(6, 2)
                    .HasColumnName("credito_diario");

                entity.Property(e => e.Fecha).HasColumnName("fecha");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Creditos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("creditos_ibfk_1");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PRIMARY");

                entity.ToTable("menus");

                entity.Property(e => e.IdMenu).HasColumnName("id_menu");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasColumnName("descripcion");

                entity.Property(e => e.NombreMenu)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_menu");

                entity.Property(e => e.Precio)
                    .HasPrecision(6, 2)
                    .HasColumnName("precio");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago)
                    .HasName("PRIMARY");

                entity.ToTable("pagos");

                entity.HasIndex(e => e.IdUsuario, "id_usuario");

                entity.Property(e => e.IdPago).HasColumnName("id_pago");

                entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Monto)
                    .HasPrecision(6, 2)
                    .HasColumnName("monto");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("pagos_ibfk_1");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.IdReserva)
                    .HasName("PRIMARY");

                entity.ToTable("reservas");

                entity.HasIndex(e => e.IdMenu, "id_menu");

                entity.HasIndex(e => e.IdUsuario, "id_usuario");

                entity.Property(e => e.IdReserva).HasColumnName("id_reserva");

                entity.Property(e => e.FechaReserva).HasColumnName("fecha_reserva");

                entity.Property(e => e.IdMenu).HasColumnName("id_menu");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.MontoTotal)
                    .HasPrecision(6, 2)
                    .HasColumnName("monto_total");

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.IdMenu)
                    .HasConstraintName("reservas_ibfk_2");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("reservas_ibfk_1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuarios");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .HasColumnName("apellido");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Puesto)
                    .HasMaxLength(100)
                    .HasColumnName("puesto");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .HasColumnName("telefono");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
