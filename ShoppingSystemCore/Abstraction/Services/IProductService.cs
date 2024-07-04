
namespace ShoppingSystemCore.Abstraction.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<bool> AddProduct(Product product);
    }
}
