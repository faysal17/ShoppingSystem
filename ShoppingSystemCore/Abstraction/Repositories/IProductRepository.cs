namespace ShoppingSystemCore.Abstraction.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<bool> AddProductAsync(Product product);
    }
}
