using UrlShortener.Shared.Exceptions.Authentication;
using UrlShortener.Service.Services.JwtGeneration;
using UrlShortener.Shared.Dto.Authentication;
using UrlShortener.Service.Services.Hashing;
using UrlShortener.Service.Services.Users;
using UrlShortener.Shared.Dto.Users;
using UrlShortener.Data.Entities;
using AutoMapper;

namespace UrlShortener.Service.Services.Authentication
{
  public class AuthenticationService : IAuthenticationService
  {
    private readonly IPasswordHashingService passwordHashingService;
    private readonly IJwtGenerationService jwtGenerationService;
    private readonly IUserService userService;
    private readonly IMapper mapper;

    public AuthenticationService(IPasswordHashingService passwordHashingService,
                                 IJwtGenerationService jwtGenerationService,
                                 IUserService userService,
                                 IMapper mapper)
    {
      this.passwordHashingService = passwordHashingService;
      this.jwtGenerationService = jwtGenerationService;
      this.userService = userService;
      this.mapper = mapper;
    }

    public async Task<string> SignInAsync(SigningInDto signingInDto)
    {
      string nickName = signingInDto.NickName!;
      string password = signingInDto.Password!;

      var user = await userService.GetUserAsync(user => user.NickName == nickName);

      if (user == null)
      {
        user = await AddUserAsync(nickName, password);
      }
      else
      {
        var verified = VerifyPassword(password, user.HashedPassword);

        if (!verified)
        {
          throw new WrongPasswordException();
        }
      }

      var jwtGenerationDto = mapper.Map<JwtGenerationDto>(user);
      return GenerateJwt(jwtGenerationDto);
    }

    private async Task<User> AddUserAsync(string nickName, string password)
    {
      var hashedPassword = HashPassword(password);

      var userAddingDto = new UserAddingDto(nickName, hashedPassword);

      return await userService.AddUserAsync(userAddingDto);
    }

    private bool VerifyPassword(string password, string storedHashedPassword)
    {
      var hashedPassword = HashPassword(password);

      return hashedPassword == storedHashedPassword;
    }

    private string HashPassword(string password)
    {
      return passwordHashingService.HashPassword(password);
    }

    private string GenerateJwt(JwtGenerationDto jwtGenerationDto)
    {
      return jwtGenerationService.GenerateJwt(jwtGenerationDto);
    }
  }
}
