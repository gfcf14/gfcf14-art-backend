using gfcf14_art_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace gfcf14_art_backend.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<Artwork> Artworks { get; set; }
  public DbSet<Link> Links { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Link>()
      .HasKey(l => new { l.ArtworkDate, l.Type, l.Url });

    modelBuilder.Entity<Link>()
      .HasOne(l => l.Artwork)
      .WithMany(a => a.Links)
      .HasForeignKey(l => l.ArtworkDate)
      .HasPrincipalKey(a => a.Date);
  }
}