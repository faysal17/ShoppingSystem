namespace ShoppingSystemWeb.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>?> GetProducts();
        Task<bool?> AddProduct(ProductViewModel product);
    }
}
