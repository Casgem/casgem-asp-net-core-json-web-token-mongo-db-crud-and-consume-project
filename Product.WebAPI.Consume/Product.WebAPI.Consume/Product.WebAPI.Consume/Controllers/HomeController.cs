using Microsoft.AspNetCore.Mvc;

namespace Product.WebAPI.Consume.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
