namespace ShoppingSystemWeb.Abstractions
{
    public interface IAuthService
    {
        Task<UserViewModel?> AuthenticateUser(UserViewModel userViewModel);
        Task<UserViewModel?> RegisterUser(UserViewModel userViewModel);
    }
}
