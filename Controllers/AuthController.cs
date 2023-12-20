using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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
                     "user", "student"
                });

            var token = service.CreateToken(user);
            return token;
        }

        [HttpGet("restrict")]
        [Authorize]
        public string Restrict()
        {
            return "Ok";
        }

        [HttpGet("admin")]
        [Authorize(Policy = "admin")]
        public string Admin()
        {
            return "admin";
        }
    }
}
