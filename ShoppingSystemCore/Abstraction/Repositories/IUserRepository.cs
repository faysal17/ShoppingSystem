namespace ShoppingSystemCore.Abstraction.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> AddUserAsync(User user);
    }
}
