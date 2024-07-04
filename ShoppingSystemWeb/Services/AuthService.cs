namespace ShoppingSystemWeb.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpService _http;

        public AuthService(IHttpService http)
        {
            _http = http;
        }

        public async Task<UserViewModel?> AuthenticateUser(UserViewModel userViewModel)
        {
            string url = @$"Auth/AuthenticateUser";
            var response = await _http.PostAsync(url, userViewModel);
            UserViewModel? registeredUser = null;

            if (response.IsSuccessStatusCode)
            {
                registeredUser = await response.Content.ReadFromJsonAsync<UserViewModel>();
            }
            else
            {
                //_logger.LogError("Internal server Error"); // TODO: change this to add modelstate error
            }

            return registeredUser;
        }

        public async Task<UserViewModel?> RegisterUser(UserViewModel userViewModel)
        {
            string url = @$"Auth/RegisterUser";
            var response = await _http.PostAsync(url, userViewModel);
            UserViewModel? registeredUser = null;

            if (response.IsSuccessStatusCode)
            {
                registeredUser = await response.Content.ReadFromJsonAsync<UserViewModel>();
            }
            else
            {
                //_logger.LogError("Internal server Error"); // TODO: change this to add modelstate error
            }

            return registeredUser;
        }
    }
}
