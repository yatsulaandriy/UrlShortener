using UrlShortener.Shared.Dto.Authentication;

namespace UrlShortener.Service.Services.JwtGeneration
{
  public interface IJwtGenerationService
  {
    string GenerateJwt(JwtGenerationDto dto);
  }
}
