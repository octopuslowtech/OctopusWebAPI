using Microsoft.AspNetCore.Mvc;

namespace OctopusWebAPI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
