using Microsoft.AspNetCore.Mvc;

namespace InventoryPOS.Controllers
{
    public class SaleController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult All()
        {
            return View();
        }

        public IActionResult Invoice()
        {
            return View();
        }
    }
}
