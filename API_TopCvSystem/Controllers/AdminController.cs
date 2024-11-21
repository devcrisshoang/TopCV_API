using Microsoft.AspNetCore.Mvc;
using API_TopCvSystem.Data.Entities;

namespace API_TopCvSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly Admin _admin;

        public AdminController()
        {
            _admin = new Admin
            {
                Username = "Admin",
                Password = "admin"
            };
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {

            if (loginRequest.Username == _admin.Username && loginRequest.Password == _admin.Password)
            {
                return Ok(new { message = "Login successful", username = _admin.Username });
            }

            return Unauthorized(new { message = "Invalid username or password" });
        }


        [HttpGet("info")]
        public IActionResult GetAdminInfo()
        {

            return Ok(new { Username = _admin.Username, Password = _admin.Password });
        }
        //API Logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout successful" });
        }

    }

    // DTO cho thông tin đăng nhập
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
