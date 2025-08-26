using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanHM_PRN232_A02_BE.Services.Interfaces;

namespace QuanHM_PRN232_A02_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStats()
        {
            var dto = await _service.GetStatisticsAsync();
            return Ok(dto);
        }
    }
}
