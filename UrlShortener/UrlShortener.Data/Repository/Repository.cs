using Microsoft.EntityFrameworkCore;
using UrlShortener.Data.Entities;
using System.Linq.Expressions;
using UrlShortener.Data.Context;

namespace UrlShortener.Data.Repository
{
  public class Repository<T> : IRepository<T> where T : Entity
  {
    private readonly UrlShortenerContext context;
    private readonly DbSet<T> set;

    public Repository(UrlShortenerContext context)
    {
      this.context = context;
      set = context.Set<T>();
    }

    public IQueryable<T> Query(params Expression<Func<T, object>>[] includes)
    {
      var query = includes
          .Aggregate<Expression<Func<T, object>>, IQueryable<T>>(set, (current, include) => current.Include(include));

      return query ?? set;
    }

    public IQueryable<T> GetAll()
    {
      return set.AsQueryable();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
      return await set.SingleOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
      return await set.FirstOrDefaultAsync(predicate);
    }

    public async Task<T> AddAsync(T entity)
    {
      var addedEntity = await set.AddAsync(entity);
      return addedEntity.Entity;
    }

    public void Remove(T entity)
    {
      set.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
      await context.SaveChangesAsync();
    }

    public async Task RemoveAllAsync()
    {
      await Task.Run(() =>
      {
        set.RemoveRange(set);
      });
    }
  }
}
