using Microsoft.AspNetCore.Mvc;

namespace OctopusWebAPI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult View()
        {
            return View();
        }
    }
}
