using UrlShortener.Shared.Dto.Authentication;

namespace UrlShortener.Service.Services.Authentication
{
  public interface IAuthenticationService
  {
    Task<string> SignInAsync(SigningInDto dto);
  }
}
