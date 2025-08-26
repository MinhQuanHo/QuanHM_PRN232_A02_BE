using Microsoft.AspNetCore.Mvc;

namespace QuanHM_PRN232_A02_FE.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index() => View();
    }
}
