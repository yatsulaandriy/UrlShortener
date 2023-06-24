using System.Linq.Expressions;
using UrlShortener.Data.Entities;
using UrlShortener.Shared.Dto.Users;

namespace UrlShortener.Service.Services.Users
{
  public interface IUserService
  {
    Task<User?> GetUserAsync(Expression<Func<User,  bool>> predicate);
    Task<User> AddUserAsync(UserAddingDto dto);
  }
}
