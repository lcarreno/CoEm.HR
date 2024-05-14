using CoEm.HR.Models;
using Microsoft.EntityFrameworkCore;

namespace CoEm.HR.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Perfil> Perfiles { get; set; }
    public DbSet<Empleador> Empleadores { get; set; }
    public DbSet<Demandante> Demandantes { get; set; }
    public DbSet<Vacante> Vacantes { get; set; }
    public DbSet<Aplicacion> Aplicaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuario");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("id");
            entity.Property(e => e.Email).HasMaxLength(255).HasColumnName("email");
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Clave).HasMaxLength(64).HasColumnName("clave");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.ToTable("perfil");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasMaxLength(250).HasColumnName("nombre");
            entity.Property(e => e.Telefono).HasMaxLength(10).HasColumnName("telefono");
            entity.Property(e => e.Direccion).HasMaxLength(1000).HasColumnName("direccion");
            entity.Property(e => e.EsEmpleador).HasColumnName("es_empleador");
            entity.HasOne(e => e.Usuario).WithOne(u => u.Perfil).HasForeignKey<Perfil>(e => e.Id).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Empleador>(entity =>
        {
            entity.ToTable("empleador");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Industria).HasMaxLength(125).HasColumnName("industria");
            entity.Property(e => e.NumeroEmpleados).HasColumnName("numero_empleados");
            entity.Property(e => e.Ubicacion).HasMaxLength(250).HasColumnName("ubicacion");
            entity.HasOne(e => e.Perfil).WithOne(u => u.Empleador).HasForeignKey<Empleador>(e => e.Id).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Demandante>(entity =>
        {
            entity.ToTable("demandante");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.NivelEducacion).HasMaxLength(125).HasColumnName("nivel_educacion");
            entity.Property(e => e.ExperienciaLaboral).HasColumnName("experiencia_laboral");
            entity.HasOne(e => e.Perfil).WithOne(u => u.Demandante).HasForeignKey<Demandante>(e => e.Id).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Vacante>(entity =>
        {
            entity.ToTable("vacante");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("id");
            entity.Property(e => e.EmpleadorId).HasColumnName("empleador_id");
            entity.Property(e => e.Titulo).HasMaxLength(250).HasColumnName("titulo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.TipoContrato).HasMaxLength(125).HasColumnName("tipo_contrato");
            entity.Property(e => e.Salario).HasPrecision(9, 0).HasColumnName("salario");
            entity.Property(e => e.FechaPublicacion).HasColumnName("fecha_publicacion");
            entity.HasOne(d => d.Empleador).WithMany(p => p.Vacantes).HasForeignKey(d => d.EmpleadorId).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Aplicacion>(entity =>
        {
            entity.ToTable("aplicacion");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd().HasColumnName("id");
            entity.Property(e => e.VacanteId).HasColumnName("vacante_id");
            entity.Property(e => e.DemandanteId).HasColumnName("demandante_id");
            entity.Property(e => e.FechaAplicacion).HasColumnName("fecha_aplicacion");
            entity.Property(e => e.Estado).HasMaxLength(125).HasColumnName("estado");
            entity.HasOne(d => d.Demandante).WithMany(p => p.Aplicaciones).HasForeignKey(d => d.DemandanteId).OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(d => d.Vacante).WithMany(p => p.Aplicaciones).HasForeignKey(d => d.VacanteId).OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
