namespace ShoppingSystemWeb.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient GetApiClient()
        {
            var httpClient = _httpClientFactory.CreateClient("ShopApi");
            return httpClient;
        }
    }
}
