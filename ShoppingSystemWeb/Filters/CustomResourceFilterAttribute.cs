using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ShoppingSystemWeb.Filters
{
    public class CustomResourceFilterAttribute : TypeFilterAttribute
    {
        public CustomResourceFilterAttribute() : base(typeof(CustomResourceFilter))
        {
        }

        private class CustomResourceFilter : IResourceFilter
        {
            private readonly ILogger<CustomResourceFilter> _logger;

            public CustomResourceFilter(ILogger<CustomResourceFilter> logger)
            {
                _logger = logger;
            }

            public void OnResourceExecuting(ResourceExecutingContext context)
            {
                var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var controllerName = actionDescriptor?.ControllerName;
                var actionName = actionDescriptor?.ActionName;

                _logger.LogInformation($"Resource Filter: Executing action: {controllerName}.{actionName}");
                _logger.LogInformation("Resource Filter: - OnResourceExecuting...");
            }

            public void OnResourceExecuted(ResourceExecutedContext context)
            {
                _logger.LogInformation("Resource Filter: - OnResourceExecuted");
            }
        }
    }
}
