using JwtAspNet.Services;
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
            var token = service.CreateToken();

            return token;
        }
    }
}
