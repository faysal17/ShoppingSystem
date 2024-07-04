using Microsoft.Net.Http.Headers;

namespace ShoppingSystemWeb.Filters
{
    public class CustomResultFilterAttribute : Attribute, IResultFilter
    {
        public readonly int _durationInSeconds = 0;

        public CustomResultFilterAttribute(int durationInSeconds)
        {
            _durationInSeconds = durationInSeconds;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var cacheProfile = new CacheProfile
            {
                Duration = _durationInSeconds
            };

            context.HttpContext.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers[HeaderNames.CacheControl] = $"public,max-age={_durationInSeconds}";
                return Task.CompletedTask;
            }, context.HttpContext);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
