using Microsoft.AspNetCore.Mvc;
using InventoryPOS.Models;

namespace InventoryPOS.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            User user = new User();
            if(user.login(email, password) == true)
            {
                var cookieOptions = new CookieOptions
                {
                    Path = "/",
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddDays(1)
                };
                Response.Cookies.Append("WO_InventoryPOS", "1", cookieOptions);
                return RedirectToAction("Index", "Home");
            } else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult Logout()
        {
            if (Request.Cookies.ContainsKey("WO_InventoryPOS"))
            {
                Response.Cookies.Delete("WO_InventoryPOS");
            }
            return RedirectToAction("Login", "Account");

        }
    }
}
