using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanHM_PRN232_A02_BE.DTOs;
using QuanHM_PRN232_A02_BE.Services.Interfaces;
using System.Security.Claims;

namespace QuanHM_PRN232_A02_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _service;
        public NewsController(INewsService service) { _service = service; }

        // Public
        [HttpGet("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublic() => Ok(await _service.GetPublicAsync());

        // Staff
        [HttpGet]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetMine()
        {
            var email = User.FindFirstValue(ClaimTypes.Name) ?? "";
            return Ok(await _service.GetMyNewsAsync(email));
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(
            string? keyword,
            short? categoryId,
            short? createdBy,
            int page = 1,
            int pageSize = 10,
            string sortBy = "createdDate",
            string sortOrder = "desc")
        {
            var (items, totalCount) = await _service.SearchAsync(keyword, categoryId, createdBy, page, pageSize, sortBy, sortOrder);

            return Ok(new
            {
                totalCount,
                page,
                pageSize,
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                data = items
            });
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetById(string id)
        {
            var email = User.FindFirstValue(ClaimTypes.Name) ?? "";
            var dto = await _service.GetByIdAsync(id, email);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Create(NewsDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Name) ?? "";
            await _service.AddAsync(dto, email);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Update(string id, NewsDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Name) ?? "";
            if (id != dto.NewsArticleId) return BadRequest();
            var ok = await _service.UpdateAsync(id, dto, email);
            return ok ? Ok(dto) : Unauthorized("You can only update your own news.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Delete(string id)
        {
            var email = User.FindFirstValue(ClaimTypes.Name) ?? "";
            var ok = await _service.DeleteAsync(id, email);
            return ok ? Ok() : NotFound("News not found or not owned by you.");
        }
    }
}
