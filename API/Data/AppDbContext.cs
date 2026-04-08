using API.models;
using Microsoft.EntityFrameworkCore;

namespace API.data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Cargo> Cargos => Set<Cargo>();
    public DbSet<Funcionario> Funcionarios => Set<Funcionario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cargo>(entity =>
        {
            // Regra de Relacionamento
            entity.HasOne(c => c.Area)
                  .WithMany(a => a.Cargos)
                  .HasForeignKey(c => c.IdArea)
                  .OnDelete(DeleteBehavior.Restrict);

            // Regra de Coluna
            entity.Property(c => c.Salario)
                  .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Funcionario>()
        .HasOne(f => f.Cargo)
        .WithMany(c => c.Funcionarios)
        .HasForeignKey(f => f.IdCargo)
        .OnDelete(DeleteBehavior.Restrict);

    }
}