using Microsoft.AspNetCore.Mvc;

namespace QuanHM_PRN232_A02_FE.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Public() => View();
        public IActionResult Manage() => View();
        public IActionResult Create() => View();
        public IActionResult Edit(string id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
