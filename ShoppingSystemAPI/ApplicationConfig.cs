namespace ShoppingSystemAPI
{
    public class ApplicationConfig : IApplicationConfig
    {
        private readonly IConfiguration _config;
        public ApplicationConfig(IConfiguration config)
        {
            _config = config;
        }

        public string ShoppingSystemConnectionString => _config.GetConnectionString("ShoppingSystem");
    }
} 
