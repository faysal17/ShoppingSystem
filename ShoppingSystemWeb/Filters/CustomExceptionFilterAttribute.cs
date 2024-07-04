namespace ShoppingSystemWeb.Filters
{
    public class CustomExceptionFilterAttribute : TypeFilterAttribute
    {
        public CustomExceptionFilterAttribute() : base(typeof(CustomExceptionFilter))
        {
        }

        public class CustomExceptionFilter : IExceptionFilter
        {
            private readonly ILogger<CustomExceptionFilter> _logger;
            private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

            public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger, ITempDataDictionaryFactory tempDataDictionaryFactory)
            {
                _logger = logger;
                _tempDataDictionaryFactory = tempDataDictionaryFactory;
            }

            public void OnException(ExceptionContext context)
            {
                _logger.LogError($"An error asf occurred: {context.Exception}");

                var tempData = _tempDataDictionaryFactory.GetTempData(context.HttpContext);

                tempData["ErrorMessage"] = context.Exception.Message;
                tempData["ErrorStackTrace"] = context.Exception.StackTrace;

                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "Index"
                }));

                context.ExceptionHandled = true;
            }
        }
    }
}
