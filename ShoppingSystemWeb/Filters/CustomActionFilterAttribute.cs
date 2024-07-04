using System.Diagnostics;

namespace ShoppingSystemWeb.Filters
{
    public class CustomActionFilterAttribute : TypeFilterAttribute
    {
        public CustomActionFilterAttribute() : base(typeof(CustomActionFilter))
        {
        }

        public class CustomActionFilter : IActionFilter
        {
            private readonly ILogger<CustomActionFilter> _logger;
            private readonly Stopwatch _stopwatch;

            public CustomActionFilter(ILogger<CustomActionFilter> logger)
            {
                _logger = logger;
                _stopwatch = new Stopwatch();
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                _stopwatch.Start();
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                _stopwatch.Stop();

                var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
                var responseSize = GetResponseSize(context.HttpContext.Response);

                _logger.LogInformation($"Action Method: {context.ActionDescriptor.DisplayName}");
                _logger.LogInformation($"Execution Time: {elapsedMilliseconds} ms");
                _logger.LogInformation($"Response Size: {responseSize} bytes");

                _stopwatch.Reset();
            }

            private long GetResponseSize(HttpResponse response)
            {
                return response.ContentLength ?? 0;
            }
        }
    }
}
