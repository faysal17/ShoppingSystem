using Microsoft.AspNetCore.Mvc;

namespace ShoppingSystemWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.Name)?.Value;
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userRole))
                {
                    HttpContext.Session.SetString("UserId", userId);
                    HttpContext.Session.SetString("UserRole", userRole);
                }

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(UserViewModel userViewModel)
        {
            UserViewModel registeredUserViewModel = await _authService.AuthenticateUser(userViewModel);
            HttpContext.Session.SetString("UserId", registeredUserViewModel.Id);
            HttpContext.Session.SetString("UserRole", registeredUserViewModel.Role);

            if (registeredUserViewModel == null)
            {
                return View("Index");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, registeredUserViewModel.Id),
                new Claim(ClaimTypes.Role, registeredUserViewModel.Role ?? "")
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserViewModel userViewModel)
        {
            UserViewModel registeredUserViewModel = await _authService.RegisterUser(userViewModel);
            if (registeredUserViewModel == null)
            {
                return View("Index");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, registeredUserViewModel.Id),
                new Claim(ClaimTypes.Role, registeredUserViewModel.Role ?? "")
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
