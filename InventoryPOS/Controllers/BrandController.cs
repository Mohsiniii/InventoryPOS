using InventoryPOS.Data;
using InventoryPOS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace InventoryPOS.Controllers
{
    public class BrandController : BaseController
    {
        private BrandDAL _brandDAL;
        public BrandController(IConfiguration configuration)
        {
            _brandDAL = new BrandDAL(configuration.GetConnectionString("DefaultConnection"));
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            var brands = _brandDAL.GettAll();
            return View(brands);
        }

        public IActionResult Add()
        {
            //return RedirectToAction("All");
            return View("Add");
        }

        public IActionResult AddBrand(Brand brand)
        {
            ViewBag.Message = 1;
            if (_brandDAL.create(brand) == true)
            {
                ViewBag.Message = 2;
            }
            else
            {
                ViewBag.Message = 3;
            }
            return RedirectToAction("All");
        }
    }
}
