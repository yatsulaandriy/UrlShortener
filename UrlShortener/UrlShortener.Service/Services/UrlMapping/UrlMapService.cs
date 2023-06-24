using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Web;
using UrlShortener.Data.Entities;
using UrlShortener.Data.Repository;
using UrlShortener.Service.Services.Url;
using UrlShortener.Service.Services.UrlShortening;
using UrlShortener.Shared.Dto.UrlMap;

namespace UrlShortener.Service.Services.UrlMapping
{
  public class UrlMapService : IUrlMapService
  {
    private readonly IUrlShortenerService urlShortenerService;
    private readonly IRepository<UrlMap> repository;
    private readonly IMapper mapper;
    public UrlMapService(IUrlShortenerService urlShortenerService,
                         IRepository<UrlMap> repository,
                         IMapper mapper)
    {
      this.urlShortenerService = urlShortenerService;
      this.repository = repository;
      this.mapper = mapper;
    }

    public async Task<UrlMap?> GetUrlMapByIdAsync(int urlMapId)
    {
      return await repository.GetByIdAsync(urlMapId);
    }

    public async Task<IEnumerable<GeneralUrlMapInformationDto>> GetGeneralUrlMapInformationsAsync()
    {
      return await repository.GetAll().Select(urlMap => mapper.Map<GeneralUrlMapInformationDto>(urlMap)).ToListAsync();
    }

    public async Task<UrlMapDetailsDto>GetUrlMapDetailsAsync(int urlMapId)
    {
      var urlMap = repository.Query()
      .Where(urlMap => urlMap.Id == urlMapId)
      .Include(urlMap => urlMap.Creator)
      .FirstOrDefault();
      ;
      var urlMapDetails = new UrlMapDetailsDto(urlMap.CreationDate, urlMap.Creator!.NickName);
      return urlMapDetails;
    }

    public async Task<UrlMap> AddUrlMapAsync(string longUrl)
    {
      string decodedUrl = HttpUtility.UrlDecode(longUrl);
      var shortenedUrl = urlShortenerService.ShortenUrl(decodedUrl);
      var urlMapAddingDto = new UrlMapAddingDto(1, decodedUrl, shortenedUrl);

      var urlMap = mapper.Map<UrlMap>(urlMapAddingDto);
      var result = await repository.AddAsync(urlMap);
      await repository.SaveChangesAsync();
      return result;
    }

    public async Task RemoveAllUrlMapsAsync()
    {
      await repository.RemoveAllAsync();
      await repository.SaveChangesAsync();
    }

    public async Task RemoveUrlMapAsync(int urlMapId)
    {
      var urlToRemove = await repository.GetByIdAsync(urlMapId);
      if (urlToRemove != null)
      {
        repository.Remove(urlToRemove);
        await repository.SaveChangesAsync();
      }
    }

    public async Task<string> GetLongUrlAsync(Expression<Func<UrlMap, bool>> predicate)
    {
      return (await repository.GetAsync(predicate)).LongUrl;
    }
  }
}
