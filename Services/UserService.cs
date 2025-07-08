using gfcf14_art_backend.Data;
using gfcf14_art_backend.Models;

namespace gfcf14_art_backend.Services;

public class UserService
{
  private readonly AppDbContext _context;

  public UserService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<User?> GetByUsernameAsync(string username)
  {
    return await _context.Users.FindAsync(username);
  }
}