namespace gfcf14_art_backend.DTOs;

public class LoginRequest
{
  public string Username { get; set; } = default!;
  public string Password { get; set; } = default!;
}