using UrlShortener.Service.Services.Authentication;
using UrlShortener.Shared.Dto.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UrlShortener.Controllers
{
  [Route("Authentication")]
  public class AuthenticationController : Controller
  {
    private readonly IAuthenticationService authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
      this.authenticationService = authenticationService;
    }

    [HttpGet("SignIn")]
    public IActionResult SignIn()
    {
      return View("SignInView");
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(SigningInDto model)
    {
      if (ModelState.IsValid)
      {
        var accessToken = await authenticationService.SignInAsync(model);

        AddJwtCookie(accessToken);
        return RedirectToAction("Index", "Home");
      }
      else
      {
        foreach (var key in ModelState.Keys)
        {
          foreach (var error in ModelState[key]!.Errors)
          {
            ModelState.AddModelError(key, error.ErrorMessage);
          }
        }
        return BadRequest();
      }
    }

    private void AddJwtCookie(string accessToken)
    {
      HttpContext.Response.Cookies.Append("Authorization", $"Bearer {accessToken}");
    }
  }
}
