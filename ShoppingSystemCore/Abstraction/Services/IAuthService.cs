namespace ShoppingSystemCore.Abstraction.Services
{
    public interface IAuthService
    {
        Task<User> ValidateUser(User loginUser);
    }
}
