using Microsoft.AspNetCore.Mvc;

namespace ShoppingSystemWeb.Controllers
{
    //[CustomAuthorizationFilter]
    public class ManageProductController : Controller
    {
        private readonly IProductService _productService;

        //private class MyClass
        //{
        //    public string Name { get; set; }
        //}

        public ManageProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddProduct(ProductViewModel productViewModel)
        {
            //MyClass obj = null;
            //string s = obj.Name;

            bool isAdded = (bool)await _productService.AddProduct(productViewModel);
            return View("Index");
        }
    }
}
