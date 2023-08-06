using System.IdentityModel.Tokens.Jwt;
using System.Resources;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using SmartChourey.BLL.Services;
using SmartChourey.BLL.Services.Interfaces;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.Services.User;
using SmartChourey.BLL.Utilites;
using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.DAL;
using SmartChourey.WebAPI.Utilities;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoureyCustomNameController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IChoureyCustomNameService _choureyCustomNameService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChoureyCustomNameController(UserManager<IdentityUser> userManager, IConfiguration configuration, IChoureyCustomNameService choureyCustomNameService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _choureyCustomNameService = choureyCustomNameService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("ChoureyCustomNameCreate")]
        public async Task<IActionResult> ChoureyCustomNameCreate(ChoureyCustomName model)
        {
            try
            {
                await _choureyCustomNameService.InsertAsync(model);

                var responseModel = new SiteImplementationViewModel()
                {
                    IsSuccessful = true,
                    Message = "Chourey Custom Name has been inserted successfully",
                };

                return Ok(responseModel);
            }
            catch (Exception)
            {
                var responseModel = new SiteImplementationViewModel()
                {
                    IsSuccessful = false,
                    Message = "An error occurred while inserting Chourey Custom Name",
                };

                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [HttpGet("getCustomChoureyNameBySiteId")]
        public async Task<ChoureyCustomName> GetCustomChoureyNameBySiteId(string siteId)
        {
            var data = await _choureyCustomNameService.GetChoureyCustomNameBySiteId(siteId);

            return data;
        }
    }
}