using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using UrlShortener.Shared.Dto.Authentication;

namespace UrlShortener.Service.Services.JwtGeneration
{
  public class JwtGenerationService : IJwtGenerationService
  { 
    private readonly SigningCredentials signingCredentials;

    public JwtGenerationService()
    {
      var signingKey = "AndriyYatsulaUrlShortener";
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
      signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }

    public string GenerateJwt(JwtGenerationDto jwtGenerationDto)
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, jwtGenerationDto.NickName),
        new Claim(ClaimTypes.Role, jwtGenerationDto.Role.ToString()),
      };

      var expirationDate = TimeSpan.FromDays(7);
      var expires = DateTime.UtcNow.Add(expirationDate);

      var jwtSecurityToken = new JwtSecurityToken
      (
        claims: claims,
        expires: expires,
        signingCredentials: signingCredentials
      );

      return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
  }
}
