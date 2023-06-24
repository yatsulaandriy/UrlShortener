namespace UrlShortener.Service.Services.UrlShortening
{
  public interface IUrlShortenerService
  {
    string ShortenUrl(string longUrl);
  }
}
