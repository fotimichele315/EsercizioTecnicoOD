using EsercizioTecnicoOD.Core.Models;
using EsercizioTecnicoOD.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EsercizioTecnicoOD.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Commessa> Commesse { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Commessa>()
            .HasMany(c => c.Jobs)
            .WithOne(j => j.Commessa)
            .HasForeignKey(j => j.CommessaId);

        modelBuilder.Entity<Job>()
            .HasOne(j => j.Item)
            .WithOne(i => i.Job)
            .HasForeignKey<Item>(i => i.JobId);
    }
}