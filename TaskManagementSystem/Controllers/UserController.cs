using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.Authoriz;

namespace TaskManagementSystem.Controllers
{
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string UserName, string Password)
        {
            var user = DummyUsers.Users
                .FirstOrDefault(u =>
                    u.Username == UserName &&
                    u.Password == Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.Username),
            //    new Claim(ClaimTypes.Role, user.Role)
            //};

            //// ✅ KEY MUST BE AT LEAST 32 CHARACTERS (256 BITS)
            //var key = new SymmetricSecurityKey(
            //    Encoding.UTF8.GetBytes("THIS_IS_A_VERY_LONG_SECRET_KEY_1234567890"));

            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(
            //    claims: claims,
            //    expires: DateTime.UtcNow.AddHours(1),
            //    signingCredentials: creds
            //);

            //return Ok(new
            //{
            //    token = new JwtSecurityTokenHandler().WriteToken(token)
            //});

            var token = GenerateJwtToken("THIS_IS_A_VERY_LONG_SECRET_KEY_1234567890",user.Username,user.Role);
            return Ok(token);
        }

        private string GenerateJwtToken(string key,string username, string role = "User", int expmin = 20)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
             new Claim(ClaimTypes.NameIdentifier, username),
             new Claim(ClaimTypes.Role, role),
            };
                
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expmin),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
