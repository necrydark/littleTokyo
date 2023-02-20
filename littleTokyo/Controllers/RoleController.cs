using littleTokyo.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace littleTokyo.Controllers
{
    public class RoleController : Controller
    {


        [Authorize(Policy = "RequireAdmin")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Manager()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdmin")]
        //[Authorize(Roles = $"{Constants.Roles.Administrator}")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
