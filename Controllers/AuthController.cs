using System.Security.Claims;
using JwtAspNet.Models;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace JwtAspNet.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : Controller
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

        [HttpGet("whoiam")]
        [Authorize]
        public object Restrict()
        {
            var userIdClaim = User.FindFirst(x => x.Type == "Id").Value;

            return new
            {
                id = User.FindFirst(x => x.Type == "Id").Value,
                name = User.FindFirst(x => x.Type == ClaimTypes.Name).Value,
                email = User.FindFirst(x => x.Type == ClaimTypes.Email).Value,
                givenName = User.FindFirst(x => x.Type == ClaimTypes.GivenName).Value,
                image = User.FindFirst(x => x.Type == "Image").Value,
            };
        }

        [HttpGet("admin")]
        [Authorize(Policy = "admin")]
        public string Admin()
        {
            return "admin";
        }
    }
}
