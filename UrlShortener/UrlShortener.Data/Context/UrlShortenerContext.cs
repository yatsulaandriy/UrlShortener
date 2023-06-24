using Microsoft.EntityFrameworkCore;
using UrlShortener.Data.Entities;
using System.Reflection;

namespace UrlShortener.Data.Context
{
  public class UrlShortenerContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<UrlMap> Urls { get; set; }

    public UrlShortenerContext(DbContextOptions options) : base(options) 
    {
      Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      options.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
  }
}
