namespace ShoppingSystemWeb.Abstractions
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync(string url, object? model);
    }
}
