using AutoMapper;
using System.Linq.Expressions;
using UrlShortener.Data.Entities;
using UrlShortener.Data.Repository;
using UrlShortener.Shared.Dto.Users;

namespace UrlShortener.Service.Services.Users
{
  public class UserService : IUserService
  {
    private readonly IRepository<User> repository;
    private readonly IMapper mapper;

    public UserService(IRepository<User> repository,
                       IMapper mapper)
    {
      this.repository = repository;
      this.mapper = mapper;
    }

    public async Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate)
    {
      return await repository.GetAsync(predicate);
    }
    public async Task<User> AddUserAsync(UserAddingDto userAddingDto)
    {
      var userToAdd = mapper.Map<User>(userAddingDto);

      var addedUser = await repository.AddAsync(userToAdd);
      await repository.SaveChangesAsync();
      return addedUser;
    }
  }
}
