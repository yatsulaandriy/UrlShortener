namespace UrlShortener.Shared.Dto.UrlMap
{
  public class UrlMapAddingDto
  {
    public int CreatorId { get; set; }
    public string LongUrl { get; set; }
    public string ShortUrl { get; set; }

    public UrlMapAddingDto(int creatorId, string longUrl, string shortUrl)
    {
      CreatorId = creatorId;
      LongUrl = longUrl;
      ShortUrl = shortUrl;
    }
  }
}
