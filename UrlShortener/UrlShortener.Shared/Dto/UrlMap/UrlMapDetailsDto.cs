namespace UrlShortener.Shared.Dto.UrlMap
{
  public class UrlMapDetailsDto
  {
    public DateTime CreationDate { get; set; }
    public string CreatorName { get; set; }

    public UrlMapDetailsDto(DateTime creationDate, string creatorName)
    {
      CreationDate = creationDate;
      CreatorName = creatorName;
    }
  }
}
