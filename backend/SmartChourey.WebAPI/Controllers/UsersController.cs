using Microsoft.AspNetCore.Mvc;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.BLL.ViewModel.Userboard;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;

        public UsersController(IUserService userService)
        {
            _usersService = userService;
        }

        [HttpGet("getUserlist")]
        public async Task<List<UserInformationViewModel>> GetUserlistAsync()
        {
            return await _usersService.GetUserList();
        }

    }
}