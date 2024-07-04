namespace ShoppingSystemServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationConfig _applicationConfig;

        public ProductService(IApplicationConfig applicationConfig)
        {
            this._unitOfWork = new UnitOfWork(applicationConfig.ShoppingSystemConnectionString);
            _applicationConfig = applicationConfig;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            IEnumerable<Product> products = await _unitOfWork.Products.GetProductsAsync();
            _unitOfWork.Commit();
            return products;
        }

        public async Task<bool> AddProduct(Product product)
        {
            bool isAdded = await _unitOfWork.Products.AddProductAsync(product);
            _unitOfWork.Commit();
            return isAdded;
        }
    }
}
