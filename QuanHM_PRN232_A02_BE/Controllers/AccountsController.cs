using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanHM_PRN232_A02_BE.DTOs;
using QuanHM_PRN232_A02_BE.Services.Interfaces;

namespace QuanHM_PRN232_A02_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _service;
        public AccountsController(IAccountService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(short id)
        {
            var dto = await _service.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountDto dto)
        {
            await _service.AddAsync(dto);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(short id, AccountDto dto)
        {
            if (id != dto.AccountId) return BadRequest();
            await _service.UpdateAsync(dto);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? Ok() : BadRequest("Cannot delete account with existing news.");
        }
    }
}
