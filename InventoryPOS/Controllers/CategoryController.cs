using InventoryPOS.Data;
using InventoryPOS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace InventoryPOS.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryDAL _categoryDAL;

        public CategoryController(IConfiguration configuration) {
            _categoryDAL = new CategoryDAL(configuration.GetConnectionString("DefaultConnection"));
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            var categories = _categoryDAL.GettAll();
            return View(categories);
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult addCategory(Category category)
        {
            ViewBag.Message = 1;
            if(_categoryDAL.create(category) == true)
            {
                ViewBag.Message = 2;
            } else
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
