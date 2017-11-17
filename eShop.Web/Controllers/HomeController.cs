using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace eShop.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("ProductSearch", "Product");
        }

    }
}
