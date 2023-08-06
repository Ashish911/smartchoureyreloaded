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
using SmartChourey.DAL;
using SmartChourey.WebAPI.Utilities;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteSpaceController : ControllerBase
    {
        private readonly ISiteSpaceAggregateService _aggregateSiteSpaceService;
        private readonly ISiteInformationService _siteInformationService;

        public SiteSpaceController(ISiteSpaceAggregateService aggregateSiteSpaceService,
            ISiteInformationService siteInformationService)
        {
            _aggregateSiteSpaceService = aggregateSiteSpaceService;
            _siteInformationService = siteInformationService;
        }

        [HttpGet("aggregateSiteSpace")]
        public async Task<SiteSpaceAggregate> AggregateSiteSpace(string siteId)
        {
            return await _aggregateSiteSpaceService.GetAggregateSpace(siteId);
        }

        [HttpGet("aggregateSiteSpaces")]
        public async Task<IEnumerable<SiteSpaceAggregateViewModel>> AggregateSiteSpaces()
        {
            return await _aggregateSiteSpaceService.GetAggregateSpaces();
        }

        [HttpPost("aggregateSiteSpacesAssign")]
        public async Task<IActionResult> InsertOrUpdateSpaceAllocation(string siteId, int space)
        {
            try
            {
                var siteInformation = await _siteInformationService.GetSiteById(siteId);
                if(siteInformation == null)
                {
                    return StatusCode(404, "Site does not exist.");
                }
                var result = await _aggregateSiteSpaceService.InsertAndUpdateSpaceAllocation(siteId, space);
                return StatusCode(200, result);
            } catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}