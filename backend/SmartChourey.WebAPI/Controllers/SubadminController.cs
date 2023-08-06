using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel;
using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.WebAPI.Utilities;
using System.Security.Claims;
using System.Security.Permissions;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubadminController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISubAdminService _subAdminService;
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubadminController(
            UserManager<IdentityUser> userManager,
            ISubAdminService subAdminService,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IEmployeeService employeeService)
        {
            _userManager = userManager;
            _subAdminService = subAdminService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _employeeService = employeeService;
        }

        [HttpGet("GetSubAdminsBySiteId")]
        public async Task<List<UserDetailViewModel>> GetSubAdminsBySiteId(string siteId)
        {
            return await _subAdminService.ListSubAdminBySiteId(siteId);
        }

        [HttpPost("CreateSubAdmin"), Authorize]
        public async Task<IActionResult> AssignSubAdmin(string email, string siteId)
        {

            if(string.IsNullOrEmpty(siteId))
            {
                var responseModel = new SiteImplementationViewModel()
                {
                    IsSuccessful = false,
                    Message = "SieId is empty."
                };

                return BadRequest(responseModel);
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                var responseModel = new SiteImplementationViewModel()
                {
                    IsSuccessful = false,
                    Message = "Email is empty."
                };

                return BadRequest(responseModel);
            }

            string userName = HttpContext.User.Identity.Name;
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (email == userName)
            {
                var responseModel = new SiteImplementationViewModel()
                {
                    IsSuccessful = false,
                    Message = "Cannot assign subadmin to own email."
                };

                return BadRequest(responseModel);
            }

            var employeeCheck = await _userService.CheckUserExistAsync(email);
            var user = await _userManager.FindByEmailAsync(email);

            if (employeeCheck.Email == null)
            {
                // Send email to the subadmin
                var userApp = new IdentityUser { UserName = email, Email = email, LockoutEnabled = false };
                var result = await _userManager.CreateAsync(userApp, "Nait@1234");

                if (result.Succeeded)
                {
                    var adminUserId = userId;
                    var subAdminUserId = userApp.Id;
                    var adminUserName = userName;

                    await _userManager.AddToRoleAsync(userApp, Roles.SubAdmin);
                    var resultSubAdmin = await _employeeService.AssignSubAdmin(siteId.Trim(), adminUserId, adminUserName, "", subAdminUserId);

                    if ((bool)resultSubAdmin.IsSuccessful)
                    {
                        return Ok();
                    }
                }
            }
            else
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                {
                    var responseModel = new SiteImplementationViewModel()
                    {
                        IsSuccessful = false,
                        Message = "Email is an admin."
                    };

                    return BadRequest(responseModel);
                }

                if (await _userService.IsSubAdminBySiteId(siteId, employeeCheck.UserId))
                {
                    var responseModel = new SiteImplementationViewModel()
                    {
                        IsSuccessful = false,
                        Message = "Email is already a subadmin."
                    };

                    return BadRequest(responseModel);
                }

                // Update the EmployeeDetail and UserSiteDetail
                var result = await _employeeService.AssignSubAdmin(siteId.Trim(), email, userName, "", userId);

                if ((bool)result.IsSuccessful)
                {
                    foreach (var item in roles)
                    {
                        await _userManager.RemoveFromRoleAsync(user, item);
                    }

                    await _userManager.AddToRoleAsync(user, Roles.SubAdmin);
                    return Ok();
                }
            }

            var errorResponseModel = new SiteImplementationViewModel()
            {
                IsSuccessful = false,
                Message = "An error occurred."
            };

            return StatusCode(StatusCodes.Status500InternalServerError, errorResponseModel);
        }

        [HttpDelete("DeleteSiteSubAdmin"), Authorize]
        public async Task<IActionResult> DeleteSiteSubAdmin(long id)
        {
            try
            {
                var isSuccess = await _subAdminService.DeleteSiteSubAdmin(id);
                if (!isSuccess)
                {
                    return StatusCode(404, "User Not Found. Delete Failed!");
                }

                return StatusCode(200, "Deleted Successfully");
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}