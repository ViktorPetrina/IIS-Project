using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MobilePhoneSpecsApi.DTOs;
using MobilePhoneSpecsApi.Models;
using MobilePhoneSpecsApi.Utilities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MobilePhoneSpecsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly SpecificationsDbContext _context;
        private readonly IMapper _mapper;

        public UserController(IConfiguration config, SpecificationsDbContext context, IMapper mapper)
        {
            _config = config;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NotFound("There are no users registered.");
            }

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(userDtos);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (_context.Users.Any(u => u.Username == dto.Username))
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Created("", new { user.Username });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            var accessToken = TokenUtils.GenerateAccessToken(user.Username, _config);
            var refreshToken = TokenUtils.GenerateRefreshToken();

            await _context.RefreshTokens.AddAsync(new RefreshToken
            {
                UserName = user.Username,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:RefreshTokenDays"]!))
            });
            await _context.SaveChangesAsync();

            return Ok(new { accessToken, refreshToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var storedToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken && !x.Revoked);

            if (storedToken == null || storedToken.Expires < DateTime.UtcNow)
            {
                return Unauthorized();
            }
                
            var accessToken = TokenUtils.GenerateAccessToken(storedToken.UserName, _config);
            storedToken.Token = TokenUtils.GenerateRefreshToken();
            storedToken.Expires = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:RefreshTokenDays"]!));
            await _context.SaveChangesAsync();

            return Ok(new { accessToken, refreshToken = storedToken.Token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            var storedToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == refreshToken);
            if (storedToken != null)
            {
                storedToken.Revoked = true;
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
