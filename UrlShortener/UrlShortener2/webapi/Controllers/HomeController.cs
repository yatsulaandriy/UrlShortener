using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using System.Diagnostics;

namespace UrlShortener.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      var accessToken = Request.Cookies["Authorization"];

      if (!string.IsNullOrEmpty(accessToken))
      {
        return View();
      }

      return RedirectToAction("SignIn", "Authentication");
    }

    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult ShortUrlsTable()
    {
      return Redirect("https://localhost:3000/");
    }

    public IActionResult About()
    {
      return null;
    }
  }
}