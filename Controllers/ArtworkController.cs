using Microsoft.AspNetCore.Mvc;
using gfcf14_art_backend.Models;
using gfcf14_art_backend.Services;
using gfcf14_art_backend.Utils;

namespace gfcf14_art_backend.Controllers;

[ApiController]
[Route("api/gfcf14-art/artworks")]
public class ArtworkController : ControllerBase
{
  private readonly ArtworkService _service;
  private readonly JwtUtil _jwtUtil;

  public ArtworkController(ArtworkService service, JwtUtil jwtUtil)
  {
    _service = service;
    _jwtUtil = jwtUtil;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Artwork>>> GetAll()
  {
    var artworks = await _service.GetAllAsync();
    return Ok(artworks);
  }

  [HttpGet("{date}")]
  public async Task<ActionResult<Artwork>> GetByDate(string date)
  {
    var artwork = await _service.GetByDateAsync(date);
    if (artwork == null)
    {
      return NotFound();
    }

    return Ok(artwork);
  }

  [HttpPost]
  public async Task<ActionResult<Artwork>> Create([FromBody] Artwork artwork)
  {
    var authHeader = Request.Headers["Authorization"].FirstOrDefault();

    if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
    {
      return Unauthorized("Missing or invalid token format");
    }

    var token = authHeader.Substring("Bearer ".Length).Trim();

    if (!_jwtUtil.IsTokenValid(token) || !_jwtUtil.CanPost(token))
    {
      return Forbid("Invalid or expired token");
    }

    var created = await _service.CreateArtworkAsync(artwork);
    return Ok(new { message = $"Artwork {artwork.Title} successfully created" });
  }
}