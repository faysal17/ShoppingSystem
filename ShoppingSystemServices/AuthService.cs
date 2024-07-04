namespace ShoppingSystemServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User?> ValidateUser(User loginUser)
        {
            User? user = await _userService.GetUserById(loginUser.Id);

            if (loginUser.Password != user.Password)
            {
                user = null;
            }

            return user;
        }
    }
}
