namespace ShoppingSystemWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpService _http;

        public ProductService(IHttpService http)
        {
            _http = http;
        }

        public async Task<IEnumerable<ProductViewModel>?> GetProducts()
        {
            string url = @$"Product/GetProducts";
            var response = await _http.GetAsync(url);
            IEnumerable<ProductViewModel>? products = null;

            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductViewModel>?>();
            }
            else
            {
                //_logger.LogError("Internal server Error"); // TODO: change this to add modelstate error
            }

            return products;
        }

        public async Task<bool?> AddProduct(ProductViewModel product)
        {
            string url = @$"Product/AddProduct";
            var response = await _http.PostAsync(url, product);
            bool isAdded = false;

            if (response.IsSuccessStatusCode)
            {
                isAdded = await response.Content.ReadFromJsonAsync<bool>();
            }
            else
            {
                //_logger.LogError("Internal server Error"); // TODO: change this to add modelstate error
            }

            return isAdded;
        }
    }
}
