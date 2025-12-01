
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpLawyers.Models;

namespace OpLawyers.DAL;

public class Contexto : IdentityDbContext<Usuario>
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Caso> Casos { get; set; }
    public DbSet<CasoDetalle> CasoDetalles { get; set; }
    public DbSet<Cita> Citas { get; set; }
    public DbSet<CitaDetalle> CitaDetalles { get; set; }
    public DbSet<HorarioDisponible> HorariosDisponibles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR",
                ConcurrencyStamp = ""
            },
            new IdentityRole
            {
                Id = "2",
                Name = "Cliente",
                NormalizedName = "CLIENTE",
                ConcurrencyStamp = ""
            }
        );

        var adminUser = new Usuario
        {
            Id = "1",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            EmailConfirmed = true,
            SecurityStamp = "",
            ConcurrencyStamp = ""
        };

        var normalUser = new Usuario
        {
            Id = "2",
            UserName = "user",
            NormalizedUserName = "USER",
            Email = "user@example.com",
            NormalizedEmail = "USER@EXAMPLE.COM",
            EmailConfirmed = true,
            SecurityStamp = "",
            ConcurrencyStamp = ""
        };

        adminUser.PasswordHash = "AQAAAAIAAYagAAAAEFckMSwoaIoT+i8P1dhWC0texCWXJM9vYogzvLShiaAto2Zb6owofXi1mYEpVcRZXA==";
        normalUser.PasswordHash = "AQAAAAIAAYagAAAAEK+QewBCKQMQoqUkUtmVEdI7MpR/XuYJ/0mXH0qKCf+PspVsJ7/naNTI3TqhdJ4b7Q==";

        modelBuilder.Entity<Usuario>().HasData(adminUser, normalUser);

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = "1", UserId = "1" },
            new IdentityUserRole<string> { RoleId = "2", UserId = "2" }
        );

        modelBuilder.Entity<Administrador>().HasData(
            new Administrador
            {
                AdministradorId = 1,
                UsuarioId = "1",
                Nombre = "Administrador Principal"
            }
        );

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Cliente)
            .WithOne(c => c.Usuario)
            .HasForeignKey<Cliente>(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Administrador)
            .WithOne(a => a.Usuario)
            .HasForeignKey<Administrador>(a => a.UsuarioId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Casos)
            .WithOne(ca => ca.Cliente)
            .HasForeignKey(ca => ca.ClienteId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Administrador>()
            .HasMany(a => a.Casos)
            .WithOne(ca => ca.Administrador)
            .HasForeignKey(ca => ca.AdministradorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Caso>()
            .HasMany(c => c.CasoDetalles)
            .WithOne(cd => cd.Caso)
            .HasForeignKey(cd => cd.CasoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Citas)
            .WithOne(ci => ci.Cliente)
            .HasForeignKey(ci => ci.ClienteId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Cita>()
            .HasMany(c => c.CitaDetalles)
            .WithOne(cd => cd.Cita)
            .HasForeignKey(cd => cd.CitaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CitaDetalle>()
            .HasOne(cd => cd.Horario)
            .WithMany(h => h.CitasDetalles)
            .HasForeignKey(cd => cd.HorarioId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        modelBuilder.Entity<CitaDetalle>()
            .HasIndex(cd => new { cd.HorarioId, cd.Fecha, cd.Bloqueado })
            .HasDatabaseName("IX_CitaDetalle_Disponibilidad");

        modelBuilder.Entity<HorarioDisponible>()
            .HasIndex(h => new { h.DiaSemana, h.HoraInicio })
            .IsUnique()
            .HasDatabaseName("IX_HorarioDisponible_DiaHora");
    }


}