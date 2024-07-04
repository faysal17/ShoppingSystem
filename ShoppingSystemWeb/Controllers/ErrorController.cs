using Microsoft.AspNetCore.Mvc;

namespace ShoppingSystemWeb.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            ErrorMessageModel? errorMessageModel = new()
            {
                Message = TempData["ErrorMessage"] as string,
                StackTrace = TempData["ErrorStackTrace"] as string
            };

            if (errorMessageModel != null)
            {
                return View(errorMessageModel);
            }

            return View();
        }
    }
}
