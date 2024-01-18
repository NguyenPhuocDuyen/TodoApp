using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApp.Models;

namespace TodoApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly UserManager<User> _userManager;

        public AuthController(IConfiguration configuration,
                               UserManager<User> userManager,
                               SignInManager<User> signInManager,
                               IPasswordHasher<User> passwordHasher)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Models.LoginRequest login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null) return BadRequest(new LoginResponse { Successful = false, Error = "Username and password are invalid." });

            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

            if (!result.Succeeded) return BadRequest(new LoginResponse { Successful = false, Error = "Username and password are invalid." });

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));
            var token = new JwtSecurityToken(
            _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(new LoginResponse { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register()
        {
            var user = new User
            {
                FullName = "NPD",
                Email = "duyen@gmail.com",
                UserName = "duyen@gmail.com" // Đặt tên người dùng tại đây, thường là email hoặc một giá trị duy nhất khác
            };

            // Kiểm tra xem người dùng đã tồn tại chưa
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return BadRequest("Email đã được đăng ký trước đó.");
            }

            // Tạo mật khẩu cho người dùng
            user.PasswordHash = _passwordHasher.HashPassword(user, "Duyen@123");

            // Thêm người dùng vào cơ sở dữ liệu
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                // Người dùng đã được tạo thành công
                return Ok("Đăng ký thành công!");
            }
            else
            {
                // Có lỗi xảy ra khi tạo người dùng, trả về lỗi
                return BadRequest("Đăng ký không thành công. Vui lòng kiểm tra thông tin và thử lại.");
            }
        }
    }
}
