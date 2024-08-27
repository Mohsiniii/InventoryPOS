using Microsoft.AspNetCore.Mvc;

namespace InventoryPOS.Controllers
{
    public class HelpController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
