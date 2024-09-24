using Entities;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            Environment.GetEnvironmentVariable("f1ConnectionString"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DriverRaces>()
        .HasKey(dr => new { dr.DriverId, dr.RaceId });
        modelBuilder.Entity<DriverRaces>()
            .HasOne(dr => dr.Driver)
            .WithMany(d => d.DriverRaces)
            .HasForeignKey(dr => dr.DriverId);
        modelBuilder.Entity<DriverRaces>()
            .HasOne(dr => dr.Race)
            .WithMany(r => r.DriverRaces)
            .HasForeignKey(dr => dr.RaceId);
    }

    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<DriverRaces> DriverRaces { get; set;}
}
