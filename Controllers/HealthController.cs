using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JwtAspNet.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController
    {
        [HttpGet]
        public string Health()
        {
            return "OK";
        }
    }
}
