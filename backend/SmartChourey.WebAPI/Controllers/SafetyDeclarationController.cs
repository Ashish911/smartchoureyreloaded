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
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.DAL;
using SmartChourey.WebAPI.Utilities;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SafetyDeclarationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISiteDeclarationService _siteDeclarationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SafetyDeclarationController(UserManager<IdentityUser> userManager, ISiteDeclarationService siteDeclarationService)
        {
            _siteDeclarationService = siteDeclarationService;
        }

        [HttpGet("siteDeclarationBySite")]
        public async Task<List<SiteDeclarationViewModel>> ListSiteDeclaration(string siteId, int viewMode)
        {
            return await _siteDeclarationService.GetSiteDeclarationBySiteIdAsync(siteId, viewMode);
        }

        [HttpGet("siteDeclarationDetailsById")]
        public async Task<SiteDeclarationViewModel> GetSiteDeclarationDetails(long siteDeclarationId, string siteId)
        {
            return await _siteDeclarationService.GetSiteDeclarationDetailsById(siteDeclarationId, siteId);
        }

        [HttpPost("insertUpdateSiteDeclaration")]
        public async Task<SiteDeclarationViewModel> InsertUpdateSiteDeclaration(SiteDeclarationViewModel model)
        {
            return await _siteDeclarationService.InsertUpdateDeclaration(model);
        }

        [HttpDelete("deleteSiteDeclaration")]
        public async Task<SiteDeclarationViewModel> DeleteSiteDeclaration(long siteDeclarationId, string siteId)
        {
            return await _siteDeclarationService.DeleteSiteDeclaration(siteDeclarationId, siteId);
        }

    }
}