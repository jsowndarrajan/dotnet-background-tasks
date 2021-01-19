using Microsoft.AspNetCore.Mvc;

namespace _1HostedService.Controllers
{
    [Route("api/{controller}")]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Ok");
        }
    }
}
