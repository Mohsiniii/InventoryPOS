using Microsoft.AspNetCore.Mvc;
using InventoryPOS.Data;

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
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
