using Microsoft.AspNetCore.Mvc;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContextController : ControllerBase
    {

        public ContextController(){}

        [HttpPost("setContext")]
        public async Task<IActionResult> SetContext(string siteId, string userId, string userName)
        {
            HttpContext.Session.SetString("siteId", siteId);
            HttpContext.Session.SetString("userId", userId);
            HttpContext.Session.SetString("userName", userName);
            return Ok(); // or any appropriate IActionResult
        }
    }
}