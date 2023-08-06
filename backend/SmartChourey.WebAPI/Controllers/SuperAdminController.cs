using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel.SuperAdmin;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperAdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public SuperAdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser(UserInformationViewModel userInformationViewModel)
        {
            try
            {
                var result = await _userService.CreateUser(userInformationViewModel);

                return Ok(result);
            } catch (Exception ex)
            {
                throw;
            }
        }
    }
}
