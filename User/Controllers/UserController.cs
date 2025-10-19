using Microsoft.AspNetCore.Mvc;

namespace User.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly string[] User = new[]
        {
            "Ashik", "Rony", "Maddy"
        };


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(4000);
            return Ok(User);
        }
    }
}
