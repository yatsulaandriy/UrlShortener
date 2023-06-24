namespace UrlShortener.Shared.Dto.UrlMap
{
  public class GeneralUrlMapInformationDto
  {
    public int Id { get; set; }
    public string LongUrl { get; set; }
    public string ShortUrl { get; set; }

    public GeneralUrlMapInformationDto(int id, string longUrl, string shortUrl)
    {
      Id = id;
      LongUrl = longUrl;
      ShortUrl = shortUrl;
    }
  }
}
