using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace gfcf14_art_backend.Utils;

public class JwtUtil
{
    private readonly string _jwtSecret;
    private readonly int _tokenDurationInMinutes = 15;

    public JwtUtil(IConfiguration config)
    {
      _jwtSecret = config["AppSettings:JWT_SECRET"] ?? throw new Exception("JWT_SECRET not configured");
    }

    public string GenerateToken(string username, bool canPost)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(new[]
          {
            new Claim(ClaimTypes.Name, username),
            new Claim("canPost", canPost.ToString())
          }),
          Expires = DateTime.UtcNow.AddMinutes(_tokenDurationInMinutes),
          SigningCredentials = new SigningCredentials(
              new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool IsTokenValid(string token)
    {
      return ValidateTokenAndGetUsername(token) != null;
    }

    public string? ValidateTokenAndGetUsername(string token)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes(_jwtSecret);

      try
      {
        var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero
        }, out _);

        return principal.Identity?.Name;
      }
      catch
      {
        return null;
      }
    }

    public bool CanPost(string token)
    {
      var handler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes(_jwtSecret);

      try
      {
        var claims = handler.ValidateToken(token, new TokenValidationParameters
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero
        }, out _).Claims;

        return bool.TryParse(claims.FirstOrDefault(c => c.Type == "canPost")?.Value, out var result) && result;
      }
      catch
      {
        return false;
      }
    }
}
