using Microsoft.AspNetCore.Mvc;

namespace InventoryPOS.Controllers
{
    public class HelpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
