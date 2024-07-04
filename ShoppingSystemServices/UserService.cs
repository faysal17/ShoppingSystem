namespace ShoppingSystemServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationConfig _applicationConfig;

        public UserService(IApplicationConfig applicationConfig)
        {
            this._unitOfWork = new UnitOfWork(applicationConfig.ShoppingSystemConnectionString);
            _applicationConfig = applicationConfig;
        }

        public async Task<User> GetUserById(string id)
        {
            User user = await _unitOfWork.Users.GetUserByIdAsync(id);
            _unitOfWork.Commit();
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            User registeredUser = await _unitOfWork.Users.AddUserAsync(user);
            _unitOfWork.Commit();
            return registeredUser;
        }
    }
}
