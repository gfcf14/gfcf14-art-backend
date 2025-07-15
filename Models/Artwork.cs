using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace gfcf14_art_backend.Models;

[Table("artworks")]
public class Artwork
{
  [Key]
  [Column("date")]
  public string Date { get; set; } = default!;
  [Column("title")]
  public string Title { get; set; } = default!;
  [Column("description")]
  public string Description { get; set; } = default!;
  [Column("image")]
  public string Image { get; set; } = default!;

  // for db context navigation
  public List<Link> Links { get; set; } = new();
}

[Table("artwork_links")]
public class Link
{
  [Column("artwork_date")]
  public string ArtworkDate { get; set; } = default!;
  [Column("type")]
  public string Type { get; set; } = default!;
  [Column("url")]
  public string Url { get; set; } = default!;

  // for db context navigation (optional when creating)
  [JsonIgnore]
  [ValidateNever]
  public Artwork? Artwork { get; set; } = default!;
}