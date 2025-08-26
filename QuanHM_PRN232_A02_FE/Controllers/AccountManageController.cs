using Microsoft.AspNetCore.Mvc;

namespace QuanHM_PRN232_A02_FE.Controllers
{
    public class AccountManageController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Create() => View();
        public IActionResult Edit(string id) { ViewBag.Id = id; return View(); }

        public IActionResult Profile() => View();
    }
}
