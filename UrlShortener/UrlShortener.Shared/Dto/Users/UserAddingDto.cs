using UrlShortener.Shared.Enumerations;

namespace UrlShortener.Shared.Dto.Users
{
  public class UserAddingDto
  {
    public string NickName { get; set; }
    public string HashedPassword { get; set; }

    public UserAddingDto(string nickName, string hashedPassword)
    {
      NickName = nickName;
      HashedPassword = hashedPassword;
    }
  }
}
