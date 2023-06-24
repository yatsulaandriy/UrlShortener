using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Shared.Dto.Authentication
{
  public class SigningInDto
  {
    [Required(ErrorMessage = "Nickname is required")]
    public string? NickName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
  }
}
