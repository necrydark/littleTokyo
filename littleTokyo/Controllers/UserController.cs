using Microsoft.AspNetCore.Mvc;

namespace littleTokyo.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
