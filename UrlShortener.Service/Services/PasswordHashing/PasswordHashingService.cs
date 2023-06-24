using Crypt = BCrypt.Net.BCrypt;

namespace UrlShortener.Service.Services.Hashing
{
  public class PasswordHashingService : IPasswordHashingService
  {
    public string HashPassword(string password)
    {
      return Crypt.HashPassword(password, 12);
    }
  }
}
