using Microsoft.AspNetCore.Mvc;

namespace QuanHM_PRN232_A02_FE.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View();
        public IActionResult Register() => View();
    }
}
