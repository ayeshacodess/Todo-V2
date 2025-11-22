using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Todo.Application.Services.Users.Dtos;
using Todo.Infrastructure.Identity;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto regDto)
        {
            var user = new ApplicationUser
            {
                FirstName = regDto.FirstName,
                LastName = regDto.LastName,
                NormalizedUserName = regDto.UserName,
                UserName = regDto.UserName,
                Email = regDto.Email
            };

            var res = await _userManager.CreateAsync(user, regDto.Password);

            if (!res.Succeeded)
                return BadRequest(res.Errors);

            return Ok("User created");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto lDto)
        {
            var user = await _userManager.FindByEmailAsync(lDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, lDto.Password))
                return Unauthorized();

            var token = GenerateJwt(user);

            return Ok(new { token });

        }

        private string GenerateJwt(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
