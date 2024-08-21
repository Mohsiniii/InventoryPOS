using Microsoft.AspNetCore.Mvc;
using InventoryPOS.Data;
using InventoryPOS.Models;

namespace InventoryPOS.Controllers
{
    public class ProductController : Controller
    {
        private ProductDAL _productDAL;
        private CategoryDAL _categoryDAL;
        private BrandDAL _brandDAL;
        public ProductController(IConfiguration configuration) {
            _productDAL = new ProductDAL(configuration.GetConnectionString("DefaultConnection"));
            _categoryDAL = new CategoryDAL(configuration.GetConnectionString("DefaultConnection"));
            _brandDAL = new BrandDAL(configuration.GetConnectionString("DefaultConnection"));
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            var products = _productDAL.GettAll();
            return View(products);
        }

        public IActionResult Add()
        {
            var categories = _categoryDAL.GettAll();
            var brands = _brandDAL.GettAll();
            var product = new Product
            {
                categories = categories,
                brands = brands
            };
            return View(product);
        }

        public IActionResult AddProduct(Product product)
        {
            ViewBag.Message = 1;
            if (_productDAL.create(product) == true)
            {
                ViewBag.Message = 2;
            }
            else
            {
                ViewBag.Message = 3;
            }
            return RedirectToAction("All");
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
