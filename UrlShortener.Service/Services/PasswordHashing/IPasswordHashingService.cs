namespace UrlShortener.Service.Services.Hashing
{
  public interface IPasswordHashingService
  {
    string HashPassword(string stringToHash);
  }
}
