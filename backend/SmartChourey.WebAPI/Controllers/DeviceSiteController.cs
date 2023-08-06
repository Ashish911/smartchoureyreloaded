using Microsoft.AspNetCore.Mvc;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.DAL;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceSiteController : ControllerBase
    {
        private readonly IDeviceSiteService _deviceSiteService;

        public DeviceSiteController(IDeviceSiteService deviceSiteService)
        {
            _deviceSiteService = deviceSiteService;
        }

        [HttpPost("assignDeviceToSite")]
        public async Task<IActionResult> AssignDeviceToSite(DeviceSiteMapperViewModel model)
        {
            var result = await _deviceSiteService.AssignDeviceToSite(model);
            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("getDeviceRegistrations")]
        public async Task<DeviceSiteListViewModel> GetDeviceRegistrations()
        {
            return await _deviceSiteService.GetDeviceRegistrations();
        }

        [HttpPut("updateDeviceRegistration")]
        public async Task<IActionResult> UpdateDeviceRegistration(DeviceRegistrationUpdateViewModel model)
        {
            var result = await _deviceSiteService.UpdateDeviceRegistration(model);
            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("deleteDeviceRegistration")]
        public async Task<IActionResult> DeleteDeviceRegistration(int deviceRegistrationId)
        {
            var result = await _deviceSiteService.DeleteDeviceRegistration(deviceRegistrationId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}