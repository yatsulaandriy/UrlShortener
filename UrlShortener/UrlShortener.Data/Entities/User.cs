using System.Text.Json.Serialization;
using UrlShortener.Shared.Enumerations;

namespace UrlShortener.Data.Entities
{
  public class User: Entity
  {
    public string NickName { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public Roles Role { get; set; }

    [JsonIgnore]
    public virtual ICollection<UrlMap>? Urls { get; set; }
  }
}
