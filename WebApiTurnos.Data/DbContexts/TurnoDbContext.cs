using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using WebApiTurnos.Data.Models;

namespace WebApiTurnos.Data.DbContexts;

public partial class TurnoDbContext : DbContext
{
    public TurnoDbContext() { }

    public TurnoDbContext(DbContextOptions<TurnoDbContext> options) : base(options)
    { }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("configurations");
        optionsBuilder.UseSqlServer(
                    "Server=DESKTOP-K4K5C8N\\SQLEXPRESS; DataBase=Turnos;Integrated Security=true; TrustServerCertificate=True; Trusted_Connection=True; User ID=admin;Password=admin123; MultipleActiveResultSets=false",
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine, LogLevel.Information);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("Branch");

            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.ToTable("Shift");

            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.DocumentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
