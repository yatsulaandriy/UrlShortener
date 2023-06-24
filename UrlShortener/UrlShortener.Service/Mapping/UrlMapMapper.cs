using AutoMapper;
using UrlShortener.Data.Entities;
using UrlShortener.Shared.Dto.UrlMap;

namespace UrlShortener.Service.Mapping
{
  public class UrlMapMapper : Profile
  {
    public UrlMapMapper()
    {
      CreateMap<UrlMapAddingDto, UrlMap>().ReverseMap();
      CreateMap<GeneralUrlMapInformationDto, UrlMap>().ReverseMap();
      CreateMap<UrlMapDetailsDto, UrlMap>().ReverseMap();
    }
  }
}
