using Microsoft.AspNetCore.Mvc;
using gfcf14_art_backend.Models;
using gfcf14_art_backend.Services;

namespace gfcf14_art_backend.Controllers;

[ApiController]
[Route("api/gfcf14-art/artworks")]
public class ArtworkController : ControllerBase
{
  private readonly ArtworkService _service;

  public ArtworkController(ArtworkService service)
  {
    _service = service;
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
    var created = await _service.CreateArtworkAsync(artwork);
    return Ok(created);
  }
}