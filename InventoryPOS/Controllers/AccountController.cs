using Microsoft.AspNetCore.Mvc;

namespace InventoryPOS.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult Loguot()
        {
            return View();
        }
    }
}
