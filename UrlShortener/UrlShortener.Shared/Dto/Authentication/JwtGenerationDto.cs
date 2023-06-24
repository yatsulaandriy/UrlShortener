using UrlShortener.Shared.Enumerations;

namespace UrlShortener.Shared.Dto.Authentication
{
  public class JwtGenerationDto
  {
    public string NickName { get; set; }
    public Roles Role { get; set; }
    public JwtGenerationDto(string nickName, Roles role)
    {
      NickName = nickName;
      Role = role;
    }
  }
}
