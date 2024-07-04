namespace ShoppingSystemData.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction) { }

        public async Task<User> GetUserByIdAsync(string id)
        {
            string sql = @$"SELECT 
                                Id,
	                            Password,
	                            Name,
                                Role
                            FROM [ShoppingSystem].[dbo].[User]
                            WHERE ID = @Id";

            var parameter = new { ID = id };
            User? user = await Connection.QuerySingleOrDefaultAsync<User>(sql, parameter, Transaction);
            return user;
        }

        public async Task<User> AddUserAsync(User user)
        {
            string sql = @$"INSERT INTO [ShoppingSystem].[dbo].[User] (Id, Password, Name, Role)
                            VALUES (@Id, @Password, @Name, @Role);";

            int rowAffected = await Connection.ExecuteAsync(sql, user, Transaction);
            if (rowAffected > 0)
            {
                return user;
            }

            return null;
        }
    } 
}
