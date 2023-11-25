using DataProcessor.Application.Common.Interfaces;
using DataProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataProcessor.Infrastructure.DataBase;

public partial class ApplicationDbContext : DbContext, IContext
{
    public ApplicationDbContext()
        => Database.EnsureCreated();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        => Database.EnsureCreated();

    public DbSet<Module> Modules { get; set; }

    public DbContext DbContext => this;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Module>(entity =>
            {
                entity
                    .ToTable("modules")
                    .HasKey(e => e.ModuleCategoryID)
                    .HasName("PRIMARY");

                entity
                    .Property(e => e.ModuleCategoryID)
                    .HasColumnType("text")
                    .HasColumnName("id")
                    .HasMaxLength(10);
                entity
                    .Property(e => e.ModuleState)
                    .HasColumnType("text")
                    .HasColumnName("state")
                    .HasMaxLength(10);
            });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
