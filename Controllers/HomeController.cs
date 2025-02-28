using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace clerk_asp_mvc_example.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult ClerkJWTS()
    {
        return Json(new {userId=HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value});
    }

    public IActionResult GatedData()
    {
        if (HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value != null)
        {
            return Json(new {message="This is gated data"});
        }
        return StatusCode(403);
    }

    

}
