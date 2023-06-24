using UrlShortener.Data.Entities;
using System.Linq.Expressions;

namespace UrlShortener.Data.Repository
{
  public interface IRepository<T> where T : Entity
  {
    IQueryable<T> Query(params Expression<Func<T, object>>[] includes);
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    void Remove(T entity);
    Task SaveChangesAsync();
    Task RemoveAllAsync();
  }
}
