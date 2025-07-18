using gfcf14_art_backend.Data;
using gfcf14_art_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace gfcf14_art_backend.Services;

public class ArtworkService
{
  private readonly AppDbContext _context;

  public ArtworkService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Artwork?> GetClosestOrEarliestAsync(string date)
  {
    var targetDate = DateTime.Parse(date);
    var allArtworks = await _context.Artworks.Include(a => a.Links).ToListAsync();

    var closest = allArtworks
      .Where(a => DateTime.TryParse(a.Date, out var d) && d <= targetDate)
      .OrderByDescending(a => DateTime.Parse(a.Date))
      .FirstOrDefault();

    if (closest != null)
    {
      return closest;
    }

    // If there are no earlier or same date artworks, return earliest available
    return allArtworks
      .Where(a => DateTime.TryParse(a.Date, out _))
      .OrderBy(a => DateTime.Parse(a.Date))
      .FirstOrDefault();
  }

  public async Task<List<Artwork>> GetAllAsync()
  {
    return await _context.Artworks
      .Include(a => a.Links)
      .OrderByDescending(a => a.Date)
      .ToListAsync();
  }

  public async Task<Artwork?> GetByDateAsync(string date)
  {
    return await _context.Artworks.FindAsync(date);
  }

  public async Task<Artwork> CreateArtworkAsync(Artwork artwork)
  {
    _context.Artworks.Add(artwork);
    await _context.SaveChangesAsync();
    return artwork;
  }
}