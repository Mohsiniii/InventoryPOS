using InventoryPOS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InventoryPOS.Controllers
{
    public class BaseController : Controller
    {
        private User _currentUser {get; set;}

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string currentController = context.RouteData.Values["controller"].ToString();
            string currentAction = context.RouteData.Values["action"].ToString();

            // Check if the cookie exists
            if (Request.Cookies.ContainsKey("WO_InventoryPOS"))
            {
                _currentUser = new User(Request.Cookies["WO_InventoryPOS"]);

                if (_currentUser.IsLoggedIn())
                {
                    // logged in
                }
                else if (currentController != "Account" || currentAction != "Login")
                {
                    context.Result = RedirectToAction("Login", "Account");
                    return;
                }
            }
            else if (currentController != "Account" || currentAction != "Login")
            {
                context.Result = RedirectToAction("Login", "Account");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
