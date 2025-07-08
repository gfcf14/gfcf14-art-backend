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

    var closest = await _context.Artworks
      .Where(a => DateTime.Parse(a.Date) <= targetDate)
      .OrderByDescending(a => DateTime.Parse(a.Date))
      .FirstOrDefaultAsync();

    if (closest != null)
    {
      return closest;
    }

    // If there are no earlier or same date artworks, return earliest available
    return await _context.Artworks
      .OrderBy(a => DateTime.Parse(a.Date))
      .FirstOrDefaultAsync();
  }

  public async Task<List<Artwork>> GetAllAsync()
  {
    return await _context.Artworks
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