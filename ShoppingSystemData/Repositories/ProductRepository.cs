namespace ShoppingSystemData.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IDbTransaction transaction) : base(transaction) { }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            string sql = @"SELECT
                        [id],
                        [title],
                        [description],
                        [price]
                    FROM [ShoppingSystem].[dbo].[Product];";

            IEnumerable<Product> products = await Connection.QueryAsync<Product>(sql, null, Transaction);
            return products;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            string sql = @"INSERT INTO [ShoppingSystem].[dbo].[Product] 
                            (
                                [id], 
                                [title], 
                                [description], 
                                [price]
                            ) 
                            VALUES 
                            (
                                @Id, 
                                @Title, 
                                @Description, 
                                @Price
                            );";

            int rowsAffected = await Connection.ExecuteAsync(sql, product, Transaction);
            return rowsAffected > 0;
        }

    }
}
