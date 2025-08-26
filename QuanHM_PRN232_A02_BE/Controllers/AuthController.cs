using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuanHM_PRN232_A02_BE.Data.Interfaces;
using QuanHM_PRN232_A02_BE.DTOs;
using QuanHM_PRN232_A02_BE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuanHM_PRN232_A02_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAccountRepository _accountRepo;

        public AuthController(IConfiguration config, IAccountRepository accountRepo)
        {
            _config = config;
            _accountRepo = accountRepo;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            // Check Admin in appsettings
            var adminEmail = _config["AdminAccount:Email"];
            var adminPass = _config["AdminAccount:Password"];
            if (login.Email == adminEmail && login.Password == adminPass)
                return Ok(new { token = GenerateToken("Admin", login.Email) });

            // Check DB
            var user = await _accountRepo.GetByEmailPasswordAsync(login.Email, login.Password);
            if (user == null) return Unauthorized();

            string role = user.AccountRole == 1 ? "Staff" :
                          user.AccountRole == 2 ? "User" : "Unknown";

            return Ok(new { token = GenerateToken(role, user.AccountEmail ?? "") });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var existing = await _accountRepo.GetByEmailAsync(dto.Email);
            if (existing != null)
            {
                return BadRequest("Email already exists!");
            }

            var newUser = new SystemAccount
            {
                AccountEmail = dto.Email,
                AccountPassword = dto.Password, 
                AccountName = dto.FullName,
                AccountRole = 2
            };

            var success = await _accountRepo.AddAsync(newUser);
            if (!success) return StatusCode(500, "Registration failed!");

            return Ok("Registered successfully!");
        }

        private string GenerateToken(string role, string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
