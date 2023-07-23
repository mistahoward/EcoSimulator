using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using backend.Models;
using backend.Services;
using backend.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly AuthService _authService;
        private readonly EcosystemDbContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(AuthService authService, EcosystemDbContext context, ILogger<UsersController> logger)
        {
            _authService = authService;
            _context = context;
            _logger = logger;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null) return BadRequest("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            if (computedHash == null) return Unauthorized("Invalid password");

            if (!computedHash.SequenceEqual(user.PasswordHash)) return Unauthorized("Invalid password");

            var usersToken = _authService.GenerateJWTToken(user);

            return Ok(new UserDto
            {
                Username = user.Username,
                Token = usersToken
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var salt = _authService.GenerateSalt();

            var passwordHash = _authService.HashPassword(registerDto.Password, salt);

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = salt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = _authService.GenerateJWTToken(user);

            return Ok(new UserDto
            {
                Username = user.Username,
                Token = token
            });
        }
    }
}