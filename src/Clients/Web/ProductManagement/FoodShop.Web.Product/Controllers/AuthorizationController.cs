using Microsoft.AspNetCore.Mvc;

namespace FoodShop.Web.Product.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
