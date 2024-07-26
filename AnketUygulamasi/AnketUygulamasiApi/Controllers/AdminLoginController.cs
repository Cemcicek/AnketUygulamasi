using DataAccess.Concrete;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace AnketUygulamasiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDBContext _applicationDBContext;
        public AdminLoginController(IConfiguration configuration, ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
            _configuration = configuration;

        }

        [HttpPost("adminlogin")]
        public async Task<IActionResult> Login(AdminLoginDto adminLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _applicationDBContext.Admins.SingleOrDefault(a => a.Email == adminLoginDto.Email);
            if (user == null || !VerifyHash(adminLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Hatalı Giriş");
            }
            return Ok(user);
        }


        [HttpPost("adminregister")]
        public async Task<IActionResult> Register(AdminRegisterDto adminRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_applicationDBContext.Admins.Any(u => u.Email == adminRegisterDto.Email))
            {
                ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılmaktadır.");
                return BadRequest(ModelState);
            }
            var admin = new Admin
            {
                Name = adminRegisterDto.Name,
                Email = adminRegisterDto.Email,
            };
            CreatePasswordHash(adminRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;

            _applicationDBContext.Add(admin);

            try
            {
                var saved = await _applicationDBContext.SaveChangesAsync();
                return Ok(saved);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedPasswordHash.Length; i++)
                {
                    if (computedPasswordHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
