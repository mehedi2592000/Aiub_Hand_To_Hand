using Microsoft.AspNetCore.Mvc;

namespace Aiub_Hand_To_Hand_MVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
