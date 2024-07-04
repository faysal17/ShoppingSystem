namespace ShoppingSystemWeb.Services
{
    public class HttpService : IHttpService
    {
        private readonly IApiService _apiService;

        #region Private Methods

        private HttpContent CreateContent(object? model)
        {
            var content = JsonSerializer.SerializeToUtf8Bytes(model);
            var byteContent = new ByteArrayContent(content);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return byteContent;
        }

        #endregion

        public HttpService(IApiService apiClient)
        {
            _apiService = apiClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var httpClient = _apiService.GetApiClient();
            var response = await httpClient.GetAsync(url);

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object? model)
        {
            var httpClient = _apiService.GetApiClient();

            var payload = CreateContent(model);
            var response = await httpClient.PostAsync(url, payload);

            return response;
        }
    }
}
