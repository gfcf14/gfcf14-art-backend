using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gfcf14_art_backend.Models;

[Table("users")]
public class User
{
  [Key]
  [Column("username")]
  public string Username { get; set; } = default!;

  [Column("password")]
  public string Password { get; set; } = default!; // stored as bcrypt hash
}