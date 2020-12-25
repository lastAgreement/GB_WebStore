using Microsoft.AspNetCore.Mvc;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), /*Authorize(Roles = Role.Administrator)*/]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
