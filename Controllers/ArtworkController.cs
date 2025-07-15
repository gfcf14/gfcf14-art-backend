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
  private readonly ILogger<ArtworkController> _logger;

  public ArtworkController(ArtworkService service, JwtUtil jwtUtil, ILogger<ArtworkController> logger)
  {
    _service = service;
    _jwtUtil = jwtUtil;
    _logger = logger;
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
    var artwork = await _service.GetClosestOrEarliestAsync(date);
    if (artwork == null)
    {
      return NotFound();
    }

    return Ok(artwork);
  }

  [HttpPost]
  public async Task<ActionResult<Artwork>> Create([FromBody] Artwork artwork)
  {
    _logger.LogInformation($"[POST] Creating artwork: {artwork.Title}");

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

    try
    {
      foreach (var link in artwork.Links)
      {
        link.ArtworkDate = artwork.Date;
      }

      var created = await _service.CreateArtworkAsync(artwork);
      return Ok(new { message = $"Artwork {artwork.Title} successfully created" });
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error creating artwork: {ex.Message}");
      _logger.LogError(ex.StackTrace);
      return StatusCode(500, "Internal Server Error");
    }
  }
}