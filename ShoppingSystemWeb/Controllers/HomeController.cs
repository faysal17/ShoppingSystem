using Microsoft.AspNetCore.Mvc;
using ShoppingSystemWeb.Models;
using System.Diagnostics;

namespace ShoppingSystemWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!(User != null && User.Identity != null && User.Identity.IsAuthenticated))
            {
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                var userId = User.FindFirst(ClaimTypes.Name)?.Value;
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userRole))
                {
                    HttpContext.Session.SetString("UserId", userId);
                    HttpContext.Session.SetString("UserRole", userRole);
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}