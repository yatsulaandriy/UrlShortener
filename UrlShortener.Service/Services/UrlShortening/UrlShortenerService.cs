namespace UrlShortener.Service.Services.UrlShortening
{
  public class UrlShortenerService: IUrlShortenerService
  {
    private const string characters = "QWERTYUIOPASDFGHJKLZXCVBNM";
    private const int shortenedUrlLength = 10;
    public string ShortenUrl(string longUrl)
    {
      Console.WriteLine(longUrl);

      if (!Uri.TryCreate(longUrl, UriKind.Absolute, out var cuttedUrl)) 
      {
        throw new Exception("This is not valid url");
      }

      Console.WriteLine(cuttedUrl);

      var random = new Random();
      var entries = new char[shortenedUrlLength];

      for(int i = 0; i < shortenedUrlLength; i++) 
      {
        entries[i] = characters[random.Next(characters.Length)];
      }

      var result = new string(entries);
      return result;
    }
  }
}
