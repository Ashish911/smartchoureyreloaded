using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel;
using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.User;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ISiteService _siteService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SiteController(UserManager<IdentityUser> userManager, IConfiguration configuration, ISiteService siteService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _siteService = siteService;
            _httpContextAccessor = httpContextAccessor;
        }

        //        [HttpPost("siteCreate")]
        //        public async Task<IActionResult> SiteCreate(SiteImplementationViewModel model)
        //        {
        //            model.UserId = HttpContext.User.Identity.GetUserId();
        //            model.UserName = HttpContext.User.Identity.GetUserName();

        //            var resultEmp = await _siteService.InsertAsync(model);
        //            if (resultEmp.IsSuccessful)
        //            {
        //                //Change the Role 
        //                //UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
        //                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //                bool checkRemove = false;
        //                var RoleName = userManager.GetRoles(model.UserId);
        //                foreach (var item in RoleName)
        //                {
        //                    userManager.RemoveFromRole(model.UserId, item);
        //                    checkRemove = true;
        //                }
        //                if (checkRemove)
        //                {
        //                    userManager.AddToRole(model.UserId, JapanSite.Web.SecurityAttributes.Roles.Admin);

        //                }
        //return
        //                    }
        //        }
        [HttpPost("siteCreate")]
        public async Task<IActionResult> SiteCreate(SiteCreateViewModel model)
        {
            try
            {
                //model.UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                //model.UserName = _httpContextAccessor.HttpContext.User.Identity.Name;
                var resultEmp = await _siteService.InsertAsync(model);

                if (resultEmp.IsSuccessful)
                {
                    //Change the Role 
                    //var user = await userManager.FindByIdAsync(model.UserId);

                    var userManager = _httpContextAccessor.HttpContext.RequestServices.GetService(typeof(UserManager<IdentityUser>)) as UserManager<IdentityUser>;
                    var user = await userManager.FindByIdAsync(model.UserId);
                    var userRoles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(model.UserId));


                    if (userRoles.Any())
                    {
                        await userManager.RemoveFromRolesAsync(user, userRoles);
                        await userManager.AddToRoleAsync(user, Utilities.Roles.Admin);
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, Utilities.Roles.Admin);
                    }

                    var responseModel = new ResponseViewModel()
                    {
                        IsSuccessful = true,
                        Message = "Site has been created successfully",
                        //SiteId = resultEmp.SiteId,
                        //SiteName = resultEmp.SiteName,
                        //SiteAccessCode = resultEmp.SiteAccessCode,
                        //UserId = model.UserId,
                        //UserName = model.UserName,
                        //UserRole = Utilities.Roles.Admin
                    };

                    return Ok(responseModel);
                }
                else
                {
                    //var responseModel = new SiteImplementationViewModel()
                    //{
                    //    IsSuccessful = false,
                    //    Message = "Failed to create site",
                    //    UserId = model.UserId,
                    //    UserName = model.UserName
                    //};

                    return BadRequest(resultEmp);
                }
            }
            catch (Exception ex)
            {
                var responseModel = new SiteImplementationViewModel()
                {
                    IsSuccessful = false,
                    Message = "An error occurred while creating site" + ex.Message,
                    UserId = model.UserId,
                    UserName = model.UserName
                };

                return StatusCode(StatusCodes.Status500InternalServerError, responseModel);
            }
        }

        [HttpGet("getSitesByAdmin")]
        public async Task<List<SiteDetailSiteComponentDetailViewModel>> GetSiteByAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            List<SiteDetailSiteComponentDetailViewModel> sites = new List<SiteDetailSiteComponentDetailViewModel>();
            if (user != null)
            {
                var roleItems = await _userManager.GetRolesAsync(user);
                var roleName = roleItems.FirstOrDefault();
                sites = await _siteService.GetSitesByAdmin(userId, roleName);
            }
            return sites;
        }

        [HttpGet("getSiteDetails")]
        public async Task<SiteDetailSiteComponentDetailViewModel> GetSiteDetails(string siteId)
        {
            var site = await _siteService.GetSitesDetails(siteId);
            return site;
        }

        [HttpPost("setSiteContext")]
        public async Task<IActionResult> SetSiteContext([FromBody] string siteId)
        {
            HttpContext.Session.SetString("siteId", siteId);
            return Ok(); // or any appropriate IActionResult
        }

        [HttpGet("getSiteDetailsForUsersByQR")]
        public async Task<SiteImplementationViewModel> GetSiteDetailsForUsersByQR(string QRCode, string latitude, string longitude, string currentTime)
        {
            var model = new SiteImplementationViewModel();
            model.UserId = HttpContext.Session.GetString("userId");
            model.UserName = HttpContext.Session.GetString("userName");

            if (QRCode != null)
            {
                model = await _siteService.SiteDetailByQRCodeAsync(QRCode.Trim(), model.UserId, model.UserName, latitude, longitude);
                if (model.QrCodeValue != null)
                {
                    //if (model.IsSuccessful)
                    //{
                    //    HttpCookie siteIdCookies = new HttpCookie("SiteId");
                    //    siteIdCookies.Value = model.Id.ToString();
                    //    siteIdCookies.Expires = new Utilities().GetTokyoDate().AddDays(1);
                    //    System.Web.HttpContext.Current.Response.Cookies.Add(siteIdCookies);
                    //    if (latitude != "")
                    //    {
                    //        HttpCookie Latitude = new HttpCookie("Latitude");
                    //        Latitude.Value = latitude.ToString();
                    //        Latitude.Expires = new Utilities().GetTokyoDate().AddDays(1);
                    //        System.Web.HttpContext.Current.Response.Cookies.Add(Latitude);
                    //        HttpCookie Longitude = new HttpCookie("Longitude");
                    //        Longitude.Value = longitude.ToString();
                    //        Longitude.Expires = new Utilities().GetTokyoDate().AddDays(1);
                    //        System.Web.HttpContext.Current.Response.Cookies.Add(Longitude);
                    //        HttpCookie GPRSRange = new HttpCookie("GPRSRange");
                    //        GPRSRange.Value = model.GPSRange.ToString();
                    //        GPRSRange.Expires = new Utilities().GetTokyoDate().AddDays(1);
                    //        System.Web.HttpContext.Current.Response.Cookies.Add(GPRSRange);
                    //        HttpCookie BrowseTimeTo = new HttpCookie("BrowseTimeTo");
                    //        BrowseTimeTo.Value = model.BrowseTimeTo.ToString();
                    //        BrowseTimeTo.Expires = new Utilities().GetTokyoDate().AddDays(1);
                    //        System.Web.HttpContext.Current.Response.Cookies.Add(BrowseTimeTo);
                    //        HttpCookie BrowseTimeFrom = new HttpCookie("BrowseTimeFrom");
                    //        BrowseTimeFrom.Value = model.BrowseTimeFrom.ToString();
                    //        BrowseTimeFrom.Expires = new Utilities().GetTokyoDate().AddDays(1);
                    //        System.Web.HttpContext.Current.Response.Cookies.Add(BrowseTimeFrom);
                    //    }
                    //}
                    //else
                    //{
                    //    model.Message = Resources.User.NotInOfficeTime;
                    //}
                }
                else
                {
                    model.Message = "Please Scan Valid QR Code";
                }
            }

            return model;
        }

        [HttpPut("siteUpdate")]
        public Task<SiteDetailSiteComponentDetailViewModel> UpdateSite(SiteDetailSiteComponentDetailViewModel siteDetailSiteComponentDetailViewModel)
        {
            try
            {
                var result = _siteService.UpdateSite(siteDetailSiteComponentDetailViewModel);

                return result;
            } catch (Exception ex) {
                throw;
            }
        }


    }
}