using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Transactions;
using ZooManagment.Domain.Models;

namespace ZooManagment.DataAccess;

public class ZooDbContext : DbContext
{
    public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>()
            .Property(a => a.FoodType)
            .HasConversion<string>();

        modelBuilder.Entity<Enclosure>()
            .Property(e => e.LocationType)
            .HasConversion<string>();

        modelBuilder.Entity<Enclosure>()
            .Property(e => e.EnclosureSize)
            .HasConversion<string>();

        modelBuilder.Entity<EnclosureLocationObject>()
            .HasKey(elo => new { elo.LocationObjectId, elo.EnclosureId });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Animal> Animals { get; set; }
    public DbSet<Specie> Species { get; set; }
    public DbSet<Enclosure> Enclosures { get; set; }
    public DbSet<LocationObject> LocationObjects { get; set; }
    public DbSet<EnclosureLocationObject> EnclosureLocationObjects { get; set; }
}