using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Web.User.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
