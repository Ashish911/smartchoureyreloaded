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
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.WebAPI.Utilities;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteCodeAccessController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ISiteService _siteService;
        private readonly ISiteCodeService _siteCodeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SiteCodeAccessController(UserManager<IdentityUser> userManager, IConfiguration configuration, ISiteService siteService, IHttpContextAccessor httpContextAccessor, ISiteCodeService siteCodeService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _siteService = siteService;
            _httpContextAccessor = httpContextAccessor;
            _siteCodeService = siteCodeService;
        }

        [HttpPost("generateCode")]
        public async Task<SiteCodeAccessViewModel> GenerateCode(SiteCodeAccessViewModel model)
        {
            var resultEmp = await _siteCodeService.GenerateSiteCodeAccessAsync(model);
            return resultEmp;
        }


        [HttpGet("accessSiteCreation")]
        public async Task<bool> AccessSiteCreation(string code)
        {
            var resultEmp = await _siteCodeService.AccessSiteCreation(code);
            return resultEmp;
        }


        [HttpGet("siteCodeList")]
        public async Task<List<SiteCodeAccessViewModel>> SiteCodeList()
        {
            var resultEmp = await _siteCodeService.ListSiteCodes();
            return resultEmp;
        }

        [HttpDelete("siteCodeDelete")]
        public async Task<SiteCodeAccessViewModel> Delete(long siteCodeId)
        {
            var siteCodeAccessViewModel = new SiteCodeAccessViewModel();
            try
            {
                var result = await _siteCodeService.DeleteSiteCode(siteCodeId);
                if(result == false)
                {
                    siteCodeAccessViewModel.IsSuccess = true;
                    siteCodeAccessViewModel.Message = "Site Code Id Not Found";
                    return siteCodeAccessViewModel;
                }
                

                siteCodeAccessViewModel.IsSuccess = true;
                siteCodeAccessViewModel.Message = "Successfully Code Deleted";
                return siteCodeAccessViewModel;
            }
            catch (Exception ex)
            {
                siteCodeAccessViewModel.IsSuccess = false;
                siteCodeAccessViewModel.Message = "Sorry Your Code has not been deleted";
                return siteCodeAccessViewModel;
            }
        }

    }
}