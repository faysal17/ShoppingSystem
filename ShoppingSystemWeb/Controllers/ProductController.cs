using Microsoft.AspNetCore.Mvc;

namespace ShoppingSystemWeb.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [CustomResultFilter(durationInSeconds: 20)]
        public async Task<IActionResult> Index()
        {
            //int a = 34;
            //a /= 0;
            IEnumerable<ProductViewModel>? products = await _productService.GetProducts();
            return View(products);
        }
    }
}
