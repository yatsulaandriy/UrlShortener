using AutoMapper;
using UrlShortener.Data.Entities;
using UrlShortener.Shared.Dto.Authentication;
using UrlShortener.Shared.Dto.Users;

namespace UrlShortener.Service.Mapping
{
  public class UserMapper: Profile
  {
    public UserMapper()
    {
      CreateMap<UserAddingDto, User>().ReverseMap();
      CreateMap<JwtGenerationDto, User>().ReverseMap();
    }
  }
}
