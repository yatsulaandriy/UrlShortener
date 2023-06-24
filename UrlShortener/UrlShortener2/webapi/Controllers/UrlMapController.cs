using UrlShortener.Service.Services.Url;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
  [ApiController]
  [Route("UrlMap")]
  public class UrlMapController : ControllerBase
  {
    private readonly IUrlMapService urlMapService;

    public UrlMapController(IUrlMapService urlMapService)
    {
      this.urlMapService = urlMapService;
    }

    [HttpGet("GetUrlMaps")]
    public async Task<IActionResult> GetUrlMapsAsync()
    {
      var urlMaps = await urlMapService.GetGeneralUrlMapInformationsAsync();
      return Ok(urlMaps);
    }

    [HttpPost("AddUrlMap/{longUrl}")]
    public async Task<IActionResult> AddUrlMapAsync(string longUrl)
    {
      var addedUrlMap = await urlMapService.AddUrlMapAsync(longUrl);
      return Ok(addedUrlMap);
    }

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    [HttpDelete("RemoveAllUrlMaps")]
    public async Task<IActionResult> RemoveAllUrlMapsAsync()
    {
      await urlMapService.RemoveAllUrlMapsAsync();
      return Ok();
    }

    [HttpDelete("RemoveUrlMap/{urlMapId}")]
    public async Task<IActionResult> RemoveUrlMapAsync(int urlMapId)
    {
      await urlMapService.RemoveUrlMapAsync(urlMapId);
      return Ok();
    }

    [HttpGet("RedirectToShortenedUrl")]
    public async Task<IActionResult> RedirectToShortenedUrl()
    {
      var shortUrl = HttpContext.Request.Path.ToUriComponent().Trim('/');
      var longUrl = await urlMapService.GetLongUrlAsync(x => x.ShortUrl == shortUrl);
      return Redirect(longUrl);
    }
  }
}
