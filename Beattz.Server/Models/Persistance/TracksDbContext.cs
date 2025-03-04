using Beattz.Server.Models.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beattz.Server.Models.Persistance;

public class TracksDbContext : DbContext
{
    public DbSet<TrackEntity> tracks { get; set; }

    public TracksDbContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("TRACKS_CONN"));
    }
}