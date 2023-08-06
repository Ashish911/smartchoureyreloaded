using Microsoft.AspNetCore.Mvc;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel.Userboard;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserboardController : ControllerBase
    {
        private readonly IUserboardService _userboardService;

        public UserboardController(IUserboardService userboardService)
        {
            _userboardService = userboardService;
        }

        [HttpGet("getUserboard")]
        public async Task<UserBoardDashboardViewModel> GetUserboardAsync()
        {
            string siteId = "";
            if (Request.Cookies["QRAccessSiteId"] != null)
            {
                siteId = Request.Cookies["QRAccessSiteId"].ToString();
            }
            else
            {
                siteId = Request.Cookies["SiteId"].ToString();
            }
            //var userId = HttpContext.User.Identity.Name;
            var userId = Request.Cookies["UserId"].ToString();

            return await _userboardService.GetUserBoard(siteId, userId, 1);
        }

    }
}