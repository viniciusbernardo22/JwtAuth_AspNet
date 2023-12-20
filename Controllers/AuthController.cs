using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtAspNet.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController
    {
        [HttpGet("login")]
        public string Login(AuthService service)
        {
            var user = new User(Id: 1,
                Email: "Vini123@gmail.com",
                Name: "Vinícius",
                Password: Guid.NewGuid().ToString(),
                Image: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSH9Hy0DRPnkdxOSN89rOI7IdevMB76D3pUhDHcz9d7KA&s",
                Roles: new[]
                {
                    "Admin", "User", "Author", "Student"
                });

            var token = service.CreateToken(user);
            return token;
        }
    }
}
