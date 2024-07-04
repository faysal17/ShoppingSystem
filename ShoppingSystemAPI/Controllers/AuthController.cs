using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService,
          IConfiguration configuration,
          ILogger<AuthController> logger,
          IUserService userService)
        {
            _authService = authService;
            _configuration = configuration;
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [Route("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser([FromBody] User user)
        {
            try
            {
                User validateUser = await _authService.ValidateUser(user);

                if(validateUser != null)
                {
                    return Ok(validateUser);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            try
            {
                User registeredUser = await _userService.AddUser(user);
                return Ok(registeredUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
