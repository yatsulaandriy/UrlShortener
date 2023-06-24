using System.Text.Json.Serialization;

namespace UrlShortener.Data.Entities
{
  public class UrlMap: Entity
  {
    public int CreatorId { get; set; }

    [JsonIgnore]
    public virtual User? Creator { get; }
    public string LongUrl { get; set; } = string.Empty;
    public string ShortUrl { get; set; } = string.Empty;

    public DateTime CreationDate { get; } = DateTime.UtcNow;
  }
}
