using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JwtAspNet.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public object Health()
        {
            return new 
            { 
                Data = DateTime.Now, 
                Versao = "1.0.0",
                Status = "OK",
            };
        }
    }
}
