using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public ProductController(IProductService productService,
          IConfiguration configuration,
          ILogger<AuthController> logger,
          IUserService userService)
        {
            _productService = productService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                IEnumerable<Product> products = await _productService.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                bool isAdded = await _productService.AddProduct(product);
                return Ok(isAdded);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
