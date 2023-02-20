using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace littleTokyo.Controllers
{
    public class RoleViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
