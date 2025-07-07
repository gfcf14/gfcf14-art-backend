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