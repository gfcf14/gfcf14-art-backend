using gfcf14_art_backend.DTOs;
using gfcf14_art_backend.Services;
using gfcf14_art_backend.Utils;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace gfcf14_art_backend.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
  private readonly UserService _userService;
  private readonly JwtUtil _jwtUtil;

  public AuthController(UserService userService, JwtUtil jwtUtil)
  {
    _userService = userService;
    _jwtUtil = jwtUtil;
  }

  [HttpPost("/login")]
  public async Task<IActionResult> Login([FromBody] LoginRequest request)
  {
    var user = await _userService.GetByUsernameAsync(request.Username);

    if (user != null && BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
    {
      var token = _jwtUtil.GenerateToken(user.Username, canPost: true);
      return Ok(new { token });
    }

    return Unauthorized("Invalid credentials");
  }
}