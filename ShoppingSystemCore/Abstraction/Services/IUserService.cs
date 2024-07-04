namespace ShoppingSystemCore.Abstraction.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(string id);
        Task<User> AddUser(User user);
    }
}
