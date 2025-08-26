using Microsoft.AspNetCore.Mvc;

namespace QuanHM_PRN232_A02_FE.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Public", "News");
        }
    }
}
