using System.Linq.Expressions;
using UrlShortener.Data.Entities;
using UrlShortener.Shared.Dto.UrlMap;

namespace UrlShortener.Service.Services.Url
{
  public interface IUrlMapService
  {
    Task<IEnumerable<GeneralUrlMapInformationDto>> GetGeneralUrlMapInformationsAsync();
    Task<UrlMapDetailsDto> GetUrlMapDetailsAsync(int urlMapId);
    Task<string> GetLongUrlAsync(Expression<Func<UrlMap, bool>> predicate);
    Task<UrlMap?> GetUrlMapByIdAsync(int urlMapId);
    Task<UrlMap> AddUrlMapAsync(string longUrl);
    Task RemoveUrlMapAsync(int urlMapId);
    Task RemoveAllUrlMapsAsync();
  }
}
