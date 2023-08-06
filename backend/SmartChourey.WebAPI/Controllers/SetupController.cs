using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartChourey.BLL.Configuration;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.ViewModel;
using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.WebAPI.Hubs;
using Newtonsoft.Json;
using Microsoft.AspNetCore.StaticFiles;
using SmartChourey.WebAPI.Utilities;
using static BusinessLogicLayer.Configuration.EnumHelpers;

namespace SmartChourey.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetupController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISetupService _setupService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ProgressHelper _progressHelper;
        private readonly SpaceHelper _spaceHelper;
        private readonly IHelpers _helpers;
        private readonly ISiteService _siteServices;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IEmailHelpers _emailHelpers;
        private readonly IEmployeeService _employeeService;
        private readonly IUserService _userService;
        private readonly ISiteSpaceDetailService _siteSpaceDetailService;

        public SetupController(UserManager<IdentityUser> userManager, 
            ISetupService setupService, 
            ProgressHelper progressHelper,
            SpaceHelper spaceHelper,
            IHelpers helpers, 
            IHttpContextAccessor httpContextAccessor,
            ISiteService siteService,
            IHostEnvironment hostEnvironment,
            IEmailHelpers emailHelpers,
            IEmployeeService employeeService,
            IUserService userService,
            ISiteSpaceDetailService siteSpaceDetailService)
        {
            _userManager = userManager;
            _setupService = setupService;
            _progressHelper = progressHelper;
            _spaceHelper = spaceHelper;
            _helpers = helpers;
            _httpContextAccessor = httpContextAccessor;
            _siteServices = siteService;
            _hostEnvironment = hostEnvironment;
            _emailHelpers = emailHelpers;
            _employeeService = employeeService;
            _userService = userService;
            _siteSpaceDetailService = siteSpaceDetailService;
        }

        [HttpGet("ListChoureyOne")]
        public async Task<List<ChoureyOneViewModel>> ListChoureyOne(string siteId)
        {
            var viewMode = (int)_helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode"));
            var choureOneInformationList = await _setupService.ListChoureyOne(siteId, viewMode);
            var groupData = choureOneInformationList.GroupBy(x => new { x.ChoureyOneId, x.Title, x.IsActive, x.SiteId })
                .Select(group => new ChoureyOneViewModel
                {
                    ChoureyOneId = group.Key.ChoureyOneId,
                    Title = group.Key.Title,
                    IsActive = group.Key.IsActive,
                });
            return groupData.ToList();
        }

        [HttpGet("listChoureyTwo")]
        public async Task<List<ChoureyTwoViewModel>> ListChoureyTwo(string siteId)
        {
            var viewMode = (int)_helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode"));
            var choureTwoInformationList = await _setupService.ListChoureyTwo(siteId, viewMode);
            var groupData = choureTwoInformationList.GroupBy(x => new { x.ChoureyTwoId, x.Title, x.IsActive, x.SiteId })
                .Select(group => new ChoureyTwoViewModel
                {
                    ChoureyTwoId = group.Key.ChoureyTwoId,
                    Title = group.Key.Title,
                    IsActive = group.Key.IsActive,
                });
            return groupData.ToList();
        }

        [HttpGet("ChoureyOneDetails")]
        public async Task<ChoureyOneViewModel> ChoureyOneDetails(long choureyOneId, string siteId)
        {
            return await _setupService.ChoureyOneDetails(choureyOneId, siteId, 1);
        }

        [HttpGet("ChoureyTwoDetails")]
        public async Task<ChoureyTwoViewModel> ChoureyTwoDetails(long ChoureyTwoId, string siteId)
        {
            return await _setupService.ChoureyTwoDetails(ChoureyTwoId, siteId, 1);
        }

        [HttpDelete("DeleteChoureyOne")]
        public async Task<ResponseViewModel> DeleteChoureyOne(long choureyId, string siteId)
        {
            ChoureyDeleteModel model = new ChoureyDeleteModel()
            {
                ChoureyId = choureyId,
                SiteId = siteId,
                IsChoureyOne = true
            };

            ResponseViewModel retVal = new ResponseViewModel();

            ResponseViewModel response = await _setupService.DeleteChourey(model);

            await _siteSpaceDetailService.DeleteByCategoryId(choureyId, siteId, ECategory.ChoureyOne);

            await _spaceHelper.UpdateSpaceUsed(await _helpers.GetSpaceUsed(siteId), _httpContextAccessor.HttpContext.Session.GetString("ProgressHubConnectionId"));

            retVal.IsSuccessful = response.IsSuccessful;
            retVal.Message = response.Message;
            return retVal;
        }

        [HttpPost("MultipleDeleteChourey")]
        public async Task<IActionResult> MultipleDeleteChourey([FromBody]MultipleChoureyDeleteModel multipleChoureyDeleteModel)
        {
            var listOfDeletedChoureyId = new List<long>();
            var listOfNotDeletedChoureyId = new List<long>();

            try
            {
                if(multipleChoureyDeleteModel.IsChoureyOne)
                {
                    foreach(var choureyOneId in multipleChoureyDeleteModel.ChoureyIds)
                    {
                        var response = await DeleteChoureyOne(choureyOneId, multipleChoureyDeleteModel.SiteId);
                        if (response.IsSuccessful)
                        {
                            listOfDeletedChoureyId.Add(choureyOneId);
                        }
                        else 
                        {
                            listOfNotDeletedChoureyId.Add(choureyOneId);
                        }
                    }
                } else
                {
                    foreach (var choureyTwoId in multipleChoureyDeleteModel.ChoureyIds)
                    {
                        var response = await DeleteChoureyTwo(choureyTwoId, multipleChoureyDeleteModel.SiteId);
                        if (response.IsSuccessful)
                        {
                            listOfDeletedChoureyId.Add(choureyTwoId);
                        }
                        else
                        {
                            listOfNotDeletedChoureyId.Add(choureyTwoId);
                        }
                    }
                }

                var result = new
                {
                    DeleteSuccess = listOfDeletedChoureyId,
                    DeleteFailed = listOfNotDeletedChoureyId
                };

                return StatusCode(200, result);
            } catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("DeleteChoureyTwo")]
        public async Task<ResponseViewModel> DeleteChoureyTwo(long choureyId, string siteId)
        {
            ChoureyDeleteModel model = new ChoureyDeleteModel()
            {
                ChoureyId = choureyId,
                SiteId = siteId,
                IsChoureyOne = false
            };

            ResponseViewModel retVal = new ResponseViewModel();

            ResponseViewModel response = await _setupService.DeleteChourey(model);

            await _siteSpaceDetailService.DeleteByCategoryId(choureyId, siteId, ECategory.ChoureyTwo);

            await _spaceHelper.UpdateSpaceUsed(await _helpers.GetSpaceUsed(siteId), _httpContextAccessor.HttpContext.Session.GetString("ProgressHubConnectionId"));

            retVal.IsSuccessful = response.IsSuccessful;
            retVal.Message = response.Message;
            return retVal;
        }


        [HttpGet("listDisastersBySiteId")]
        public async Task<List<DisasterViewModel>> ListDisastersBySiteId(string siteId)
        {
            var viewMode = (int)_helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode"));
            var disasterInformationList = await _setupService.ListDisaster(siteId, viewMode);
            var groupData = disasterInformationList.GroupBy(x => new { x.DisasterId, x.Title, x.IsActive, x.SiteId })
                .Select(group => new DisasterViewModel
                {
                    DisasterId = group.Key.DisasterId,
                    Title = group.Key.Title,
                    IsActive = group.Key.IsActive,
                });
            return groupData.ToList();
        }

        [HttpGet("GetDisasterDetailsById")]
        public async Task<DisasterViewModel> GetDisasterDetailsById(long disasterId, string siteId)
        {
            return await _setupService.GetDisasterDetailsById(disasterId, siteId, 1);       //view mode change : todo
        }

        [HttpGet("testSignalR")]
        public async Task<ResponseViewModel> TestSignalR()
        {
            ResponseViewModel retVal = new ResponseViewModel();
            for (int i = 0; i <= 100; i += 10)
            {
                await _progressHelper.SendProgress(i, "TestKiran");
                Thread.Sleep(1000); // Simulate delay
            }

            retVal.Message = "success";
            return retVal;
        }

        [HttpPost("choureyOneInsert"), Authorize]
        public async Task<IActionResult> InsertChoureyOne([FromForm]ChoureyOneViewModel choureyOneViewModel, IList<IFormFile> files, IList<IFormFile> videos, IList<IFormFile> thumbs, IList<IFormFile> uploads)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();

            try
            {
                var siteId = Request.Cookies["SiteId"]?.ToString();
                if (siteId == null && choureyOneViewModel.SiteId != null)
                {
                    siteId = choureyOneViewModel.SiteId;
                }

                if (siteId != null)
                {
                    var isSiteExist = await _siteServices.CheckIfSiteExist(siteId);
                    if (!isSiteExist)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = $"Site with Id {siteId} does not exist";
                        return StatusCode(404, JsonConvert.SerializeObject(responseViewModel));
                    }

                    var hasSpaceLimitExceeded = await _helpers.IsSpaceLimitExceeded(files, videos, siteId);

                    if (hasSpaceLimitExceeded)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = "Your Space Limit Exceeded";
                        return StatusCode(429, JsonConvert.SerializeObject(responseViewModel));
                    }

                    var progressHubConnectionId = _httpContextAccessor.HttpContext.Session.GetString("ProgressHubConnectionId");
                    float addedSpace = 0.00f;

                    IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

                    choureyOneViewModel.CurrentUserId = user.Id;
                    choureyOneViewModel.UserName = user.UserName;
                    choureyOneViewModel.SiteId = siteId;
                    choureyOneViewModel.ChoureyOneId = choureyOneViewModel.ChoureyOneId ?? 0;
                    choureyOneViewModel.EViewMode = _helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode"));

                    var choureyOneInformation = await _setupService.InsertUpdateChoureyOneAsync(choureyOneViewModel);

                    await _progressHelper.SendProgress(10, progressHubConnectionId);

                    var choureyOneId = choureyOneInformation.ChoureyOneId;


                    IList<Task> uploadTasks = new List<Task>();
                    var userId = choureyOneViewModel.CurrentUserId;

                    var rootPath = _hostEnvironment.ContentRootPath;
                    addedSpace = await _setupService.ProcessUploads(choureyOneViewModel, uploads, addedSpace, uploadTasks, rootPath);
                    addedSpace = await _setupService.ProcessFiles(choureyOneViewModel, files, addedSpace, uploadTasks, rootPath);

                    await _progressHelper.SendProgress(40, progressHubConnectionId);

                    addedSpace = await _setupService.ProcessVideos(choureyOneViewModel, videos, thumbs, addedSpace, uploadTasks, rootPath);

                    await _progressHelper.SendProgress(60, progressHubConnectionId);

                    await _setupService.UpdateSiteSpaceAggregate(siteId, addedSpace);

                    await _progressHelper.SendProgress(100, progressHubConnectionId);

                    var spaceUsed = await _helpers.GetSpaceUsed(siteId);

                    await _spaceHelper.UpdateSpaceUsed(spaceUsed, progressHubConnectionId);
                    await _emailHelpers.SendSpaceLowEmail(user.UserName, siteId);

                    await Task.WhenAll(uploadTasks.ToArray());

                    responseViewModel.IsSuccessful = true;
                    responseViewModel.Message = "Process completed Successfully";
                    return StatusCode(200, JsonConvert.SerializeObject(responseViewModel));
                }

                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = "SiteId is NULL";
                return StatusCode(400, JsonConvert.SerializeObject(responseViewModel));
            } catch (Exception ex)
            {
                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = ex.Message;
                return StatusCode(500, JsonConvert.SerializeObject(responseViewModel));
            }
        }

        [HttpPost("choureyTwoInsert"), Authorize]
        public async Task<IActionResult> InsertChoureyTwo([FromForm] ChoureyTwoViewModel choureyTwoViewModel, IList<IFormFile> files, IList<IFormFile> videos, IList<IFormFile> thumbs, IList<IFormFile> uploads)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();
            try
            {
                var siteId = Request.Cookies["SiteId"]?.ToString();
                if (siteId == null && choureyTwoViewModel.SiteId != null)
                {
                    siteId = choureyTwoViewModel.SiteId;
                }

                if(siteId != null)
                {
                    var isSiteExist = await _siteServices.CheckIfSiteExist(siteId);
                    if (!isSiteExist)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = $"Site with Id {siteId} does not exist";
                        return StatusCode(404, JsonConvert.SerializeObject(responseViewModel));
                    }

                    var hasSpaceLimitExceeded = await _helpers.IsSpaceLimitExceeded(files, videos, siteId);

                    if (hasSpaceLimitExceeded)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = "Your Space Limit Exceeded";
                        return StatusCode(429, JsonConvert.SerializeObject(responseViewModel));
                    }

                    var progressHubConnectionId = _httpContextAccessor.HttpContext.Session.GetString("ProgressHubConnectionId");
                    float addedSpace = 0.00f;

                    IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

                    choureyTwoViewModel.CurrentUserId = user.Id;
                    choureyTwoViewModel.UserName = user.UserName;
                    choureyTwoViewModel.SiteId = siteId;
                    choureyTwoViewModel.ChoureyTwoId = choureyTwoViewModel.ChoureyTwoId ?? 0;
                    choureyTwoViewModel.EViewMode = _helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode"));

                    await _progressHelper.SendProgress(10, progressHubConnectionId);

                    var choureyTwoInformation = await _setupService.InsertUpdateChoureyTwoAsync(choureyTwoViewModel);


                    IList<Task> uploadTasks = new List<Task>();

                    var rootPath = _hostEnvironment.ContentRootPath;
                    addedSpace = await _setupService.ProcessUploads(choureyTwoViewModel, uploads, addedSpace, uploadTasks, rootPath);
                    addedSpace = await _setupService.ProcessFiles(choureyTwoViewModel, files, addedSpace, uploadTasks, rootPath);

                    await _progressHelper.SendProgress(40, progressHubConnectionId);

                    addedSpace = await _setupService.ProcessVideos(choureyTwoViewModel, videos, thumbs, addedSpace, uploadTasks, rootPath);

                    await _progressHelper.SendProgress(60, progressHubConnectionId);

                    await _setupService.UpdateSiteSpaceAggregate(siteId, addedSpace);

                    await _progressHelper.SendProgress(100, progressHubConnectionId);

                    var spaceUsed = await _helpers.GetSpaceUsed(siteId);

                    await _spaceHelper.UpdateSpaceUsed(spaceUsed, progressHubConnectionId);
                    await _emailHelpers.SendSpaceLowEmail(user.UserName, siteId);

                    await Task.WhenAll(uploadTasks.ToArray());

                    responseViewModel.IsSuccessful = true;
                    responseViewModel.Message = "Process completed Successfully";
                    return StatusCode(200, JsonConvert.SerializeObject(responseViewModel));
                }

                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = "SiteId is NULL";
                return StatusCode(400, JsonConvert.SerializeObject(responseViewModel));
            } catch(Exception ex)
            {
                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = ex.Message;
                return StatusCode(500, JsonConvert.SerializeObject(responseViewModel));
            } 
        }

        [HttpPut("choureyOneEdit"), Authorize]
        public async Task<IActionResult> EditChoureyOne([FromForm] ChoureyOneViewModel choureyOneViewModel, IList<IFormFile> files, IList<IFormFile> videos, IList<IFormFile> thumbs, IList<IFormFile> uploads)
        {
            ResponseViewModel responseViewModel= new ResponseViewModel();
            try
            {
                var siteId = Request.Cookies["SiteId"]?.ToString();
                if (siteId == null && choureyOneViewModel.SiteId != null)
                {
                    siteId = choureyOneViewModel.SiteId;
                }

                if (choureyOneViewModel.ChoureyOneId == null)
                {
                    responseViewModel.IsSuccessful = false;
                    responseViewModel.Message = $"ChoureyOneId is NULL";
                    return StatusCode(400, JsonConvert.SerializeObject(responseViewModel));
                }

                if (siteId != null)
                {
                    var isSiteExist = await _siteServices.CheckIfSiteExist(siteId);
                    if (!isSiteExist)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = $"Site with Id {siteId} does not exist";
                        return StatusCode(404, JsonConvert.SerializeObject(responseViewModel));
                    }

                    var hasSpaceLimitExceeded = await _helpers.IsSpaceLimitExceeded(files, videos, siteId);

                    if (hasSpaceLimitExceeded)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = "Your Space Limit Exceeded";
                        return StatusCode(429, JsonConvert.SerializeObject(responseViewModel));
                    }

                    var progressHubConnectionId = _httpContextAccessor.HttpContext.Session.GetString("ProgressHubConnectionId");

                    await _progressHelper.SendProgress(10, progressHubConnectionId);

                    IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

                    choureyOneViewModel.CurrentUserId = user.Id;
                    choureyOneViewModel.UserName = user.UserName;
                    choureyOneViewModel.SiteId = siteId;
                    var choureyOneInformation = await _setupService.InsertUpdateChoureyOneAsync(choureyOneViewModel);

                    await _progressHelper.SendProgress(20, progressHubConnectionId);

                    IList<Task> uploadTasks = new List<Task>();
                    var rootPath = _hostEnvironment.ContentRootPath;
                    float addedSpace = 0.00f;

                    addedSpace = await _setupService.ProcessUploads(choureyOneViewModel, uploads, addedSpace, uploadTasks, rootPath);
                    addedSpace = await _setupService.ProcessFiles(choureyOneViewModel, files, addedSpace, uploadTasks, rootPath);

                    await _progressHelper.SendProgress(40, progressHubConnectionId);

                    if (choureyOneViewModel.ImageList != null)
                    {
                        foreach (var image in choureyOneViewModel.ImageList)
                        {
                            await _setupService.UpdateChoureyOnePhotoCustomName(image.CustomName, (long)choureyOneViewModel.PhotoId, choureyOneViewModel.UserName);
                        }
                    }

                    addedSpace = await _setupService.ProcessVideos(choureyOneViewModel, videos, thumbs, addedSpace, uploadTasks, rootPath);

                    await _progressHelper.SendProgress(60, progressHubConnectionId);

                    if (choureyOneViewModel.VideoList != null)
                    {
                        foreach (var video in choureyOneViewModel.VideoList)
                        {
                            await _setupService.UpdateChoureyOneVideoCustomName(video.CustomName, (long)video.VideoId, choureyOneViewModel.UserName);
                        }
                    }

                    await _progressHelper.SendProgress(70, progressHubConnectionId);

                    await _setupService.UpdateSiteSpaceAggregate(siteId, addedSpace);

                    await Task.WhenAll(uploadTasks.ToArray());

                    await _progressHelper.SendProgress(100, progressHubConnectionId);

                    var spaceHubConnectionId = _httpContextAccessor.HttpContext.Session.GetString("SpaceHubConnectionId");
                    await _spaceHelper.UpdateSpaceUsed(await _helpers.GetSpaceUsed(siteId), spaceHubConnectionId);

                    await _emailHelpers.SendSpaceLowEmail(user.UserName, siteId);

                    responseViewModel.IsSuccessful = true;
                    responseViewModel.Message = "Process completed Successfully";
                    return StatusCode(200, JsonConvert.SerializeObject(responseViewModel));
                }

                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = "SiteId is NULL";
                return StatusCode(400, JsonConvert.SerializeObject(responseViewModel));
            } catch(Exception ex)
            {
                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = ex.Message;
                return StatusCode(500, JsonConvert.SerializeObject(responseViewModel));
            }
        }

        [HttpPut("choureyTwoEdit"), Authorize]
        public async Task<IActionResult> EditChoureyTwo([FromForm] ChoureyTwoViewModel choureyTwoViewModel, IList<IFormFile> files, IList<IFormFile> videos, IList<IFormFile> thumbs, IList<IFormFile> uploads)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();
            try
            {
                var siteId = Request.Cookies["SiteId"]?.ToString();
                if (siteId == null && choureyTwoViewModel.SiteId != null)
                {
                    siteId = choureyTwoViewModel.SiteId;
                }

                if (choureyTwoViewModel.ChoureyTwoId == null)
                {
                    responseViewModel.IsSuccessful = false;
                    responseViewModel.Message = $"ChoureyTwoId is NULL";
                    return StatusCode(400, JsonConvert.SerializeObject(responseViewModel));
                }

                if (siteId != null)
                {
                    var isSiteExist = await _siteServices.CheckIfSiteExist(siteId);
                    if (!isSiteExist)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = $"Site with Id {siteId} does not exist";
                        return StatusCode(404, JsonConvert.SerializeObject(responseViewModel));
                    }

                    var hasSpaceLimitExceeded = await _helpers.IsSpaceLimitExceeded(files, videos, siteId);

                    if (hasSpaceLimitExceeded)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = "Your Space Limit Exceeded";
                        return StatusCode(429, JsonConvert.SerializeObject(responseViewModel));
                    }

                    var progressHubConnectionId = _httpContextAccessor.HttpContext.Session.GetString("ProgressHubConnectionId");

                    await _progressHelper.SendProgress(10, progressHubConnectionId);

                    IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

                    choureyTwoViewModel.CurrentUserId = user.Id;
                    choureyTwoViewModel.UserName = user.UserName;
                    choureyTwoViewModel.SiteId = siteId;
                    var choureyOneInformation = await _setupService.InsertUpdateChoureyTwoAsync(choureyTwoViewModel);

                    await _progressHelper.SendProgress(20, progressHubConnectionId);

                    IList<Task> uploadTasks = new List<Task>();
                    var rootPath = _hostEnvironment.ContentRootPath;
                    float addedSpace = 0.00f;

                    addedSpace = await _setupService.ProcessUploads(choureyTwoViewModel, uploads, addedSpace, uploadTasks, rootPath);
                    addedSpace = await _setupService.ProcessFiles(choureyTwoViewModel, files, addedSpace, uploadTasks, rootPath);

                    await _progressHelper.SendProgress(40, progressHubConnectionId);

                    if (choureyTwoViewModel.ImageList != null)
                    {
                        foreach (var image in choureyTwoViewModel.ImageList)
                        {
                            await _setupService.UpdateChoureyTwoPhotoCustomName(image.CustomName, (long)choureyTwoViewModel.PhotoId, choureyTwoViewModel.UserName);
                        }
                    }
                    await _progressHelper.SendProgress(50, progressHubConnectionId);

                    addedSpace = await _setupService.ProcessVideos(choureyTwoViewModel, videos, thumbs, addedSpace, uploadTasks, rootPath);

                    await _progressHelper.SendProgress(60, progressHubConnectionId);

                    if (choureyTwoViewModel.VideoList != null)
                    {
                        foreach (var video in choureyTwoViewModel.VideoList)
                        {
                            _setupService.UpdateChoureyTwoVideoCustomName(video.CustomName, (long)video.VideoId, choureyTwoViewModel.UserName);
                        }
                    }

                    await _progressHelper.SendProgress(70, progressHubConnectionId);

                    await _setupService.UpdateSiteSpaceAggregate(siteId, addedSpace);

                    await Task.WhenAll(uploadTasks.ToArray());

                    await _progressHelper.SendProgress(100, progressHubConnectionId);

                    var spaceHubConnectionId = _httpContextAccessor.HttpContext.Session.GetString("SpaceHubConnectionId");
                    await _spaceHelper.UpdateSpaceUsed(await _helpers.GetSpaceUsed(siteId), spaceHubConnectionId);

                    await _emailHelpers.SendSpaceLowEmail(user.UserName, siteId);

                    responseViewModel.IsSuccessful = true;
                    responseViewModel.Message = "Process completed Successfully";
                    return StatusCode(200, JsonConvert.SerializeObject(responseViewModel));
                }

                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = "SiteId is NULL";
                return StatusCode(400, JsonConvert.SerializeObject(responseViewModel));
            }
            catch (Exception ex)
            {
                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = ex.Message;
                return StatusCode(500, JsonConvert.SerializeObject(responseViewModel));
            }
        }

        [HttpPost("DisasterInsert"), Authorize]
        public async Task<IActionResult> InsertDisaster([FromForm] DisasterViewModel disasterViewModel, IList<IFormFile> files, IList<IFormFile> uploads)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();

            try
            {
                var siteId = Request.Cookies["SiteId"]?.ToString();
                if (siteId == null && disasterViewModel.SiteId != null)
                {
                    siteId = disasterViewModel.SiteId;
                }

                if (siteId != null)
                {
                    var isSiteExist = await _siteServices.CheckIfSiteExist(siteId);
                    if (!isSiteExist)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = $"Site with Id {siteId} does not exist";
                        return StatusCode(404, JsonConvert.SerializeObject(responseViewModel));
                    }

                    var hasSpaceLimitExceeded = await _helpers.IsSpaceLimitExceeded(files, new List<IFormFile>(), siteId);

                    if (hasSpaceLimitExceeded)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = "Your Space Limit Exceeded";
                        return StatusCode(429, JsonConvert.SerializeObject(responseViewModel));
                    }

                    IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

                    disasterViewModel.CurrentUserId = user.Id;
                    disasterViewModel.UserName = user.UserName;
                    disasterViewModel.SiteId = siteId;
                    disasterViewModel.EViewMode = _helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode"));
                    var disasterInformation = await _setupService.InsertUpdateDisaster(disasterViewModel);

                    var progressHubConnectionId = _httpContextAccessor.HttpContext.Session.GetString("ProgressHubConnectionId");

                    await _progressHelper.SendProgress(20, progressHubConnectionId);

                    int countInterval = 60 / files.Count();
                    int completionPercent = 30 + countInterval;

                    IList<Task> uploadTasks = new List<Task>();
                    var rootPath = _hostEnvironment.ContentRootPath;
                    float addedSpace = 0.00f;

                    if ((bool)disasterInformation.IsSuccessful!)
                    {
                        addedSpace = await _setupService.ProcessUploads(disasterViewModel, uploads, addedSpace, uploadTasks, rootPath);

                        await _progressHelper.SendProgress(completionPercent, progressHubConnectionId);
                        completionPercent += countInterval;

                        addedSpace = await _setupService.ProcessFiles(disasterViewModel, files, addedSpace, uploadTasks, rootPath);

                        await _progressHelper.SendProgress(80, progressHubConnectionId);

                        await _setupService.UpdateSiteSpaceAggregate(siteId, addedSpace);

                        await _progressHelper.SendProgress(100, progressHubConnectionId);

                        var spaceHubConnectionId = _httpContextAccessor.HttpContext.Session.GetString("SpaceHubConnectionId");
                        await _spaceHelper.UpdateSpaceUsed(await _helpers.GetSpaceUsed(siteId), spaceHubConnectionId);

                        await _emailHelpers.SendSpaceLowEmail(user.UserName, siteId);
                    }

                    responseViewModel.IsSuccessful = true;
                    responseViewModel.Message = $"Operation Successfull";
                    return StatusCode(200, JsonConvert.SerializeObject(responseViewModel));
                }

                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = $"Site with Id {siteId} does not exist";
                return StatusCode(404, JsonConvert.SerializeObject(responseViewModel));
            } catch (Exception ex)
            {
                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = ex.Message;
                return StatusCode(404, JsonConvert.SerializeObject(responseViewModel));
            }
        }

        [HttpPut("DisasterEdit"), Authorize]
        public async Task<IActionResult> EditDisaster([FromForm] DisasterViewModel disasterViewModel, IList<IFormFile> files, IList<IFormFile> uploads)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();

            try
            {
                var siteId = Request.Cookies["SiteId"]?.ToString();
                if (siteId == null && disasterViewModel.SiteId != null)
                {
                    siteId = disasterViewModel.SiteId;
                }

                if (disasterViewModel.DisasterId == null)
                {
                    responseViewModel.IsSuccessful = false;
                    responseViewModel.Message = $"DisasterId is NULL";
                    return StatusCode(400, JsonConvert.SerializeObject(responseViewModel));
                }

                if (siteId != null)
                {
                    var isSiteExist = await _siteServices.CheckIfSiteExist(siteId);
                    if (!isSiteExist)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = $"Site with Id {siteId} does not exist";
                        return StatusCode(404, JsonConvert.SerializeObject(responseViewModel));
                    }

                    var hasSpaceLimitExceeded = await _helpers.IsSpaceLimitExceeded(files, new List<IFormFile>(), siteId);

                    if (hasSpaceLimitExceeded)
                    {
                        responseViewModel.IsSuccessful = false;
                        responseViewModel.Message = "Your Space Limit Exceeded";
                        return StatusCode(429, JsonConvert.SerializeObject(responseViewModel));
                    }

                    IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

                    disasterViewModel.CurrentUserId = user.Id;
                    disasterViewModel.UserName = user.UserName;
                    disasterViewModel.SiteId = siteId;
                    disasterViewModel.EViewMode = _helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode"));
                    var disasterInformation = await _setupService.InsertUpdateDisaster(disasterViewModel);

                    var progressHubConnectionId = _httpContextAccessor.HttpContext.Session.GetString("ProgressHubConnectionId");

                    await _progressHelper.SendProgress(20, progressHubConnectionId);

                    int countInterval = 60 / files.Count();
                    int completionPercent = 30 + countInterval;

                    IList<Task> uploadTasks = new List<Task>();
                    var rootPath = _hostEnvironment.ContentRootPath;
                    float addedSpace = 0.00f;

                    if ((bool)disasterInformation.IsSuccessful!)
                    {
                        addedSpace = await _setupService.ProcessUploads(disasterViewModel, uploads, addedSpace, uploadTasks, rootPath);

                        await _progressHelper.SendProgress(completionPercent, progressHubConnectionId);
                        completionPercent += countInterval;

                        addedSpace = await _setupService.ProcessFiles(disasterViewModel, files, addedSpace, uploadTasks, rootPath);

                        await _progressHelper.SendProgress(60, progressHubConnectionId);

                        if (disasterViewModel.ImageList != null)
                        {
                            foreach (var image in disasterViewModel.ImageList)
                            {
                                _setupService.UpdateDisasterPhotoCustomName(image.CustomName, (long)image.PhotoId, disasterViewModel.UserName);
                            }
                        }

                        await _setupService.UpdateSiteSpaceAggregate(siteId, addedSpace);

                        await _progressHelper.SendProgress(100, progressHubConnectionId);

                        var spaceHubConnectionId = _httpContextAccessor.HttpContext.Session.GetString("SpaceHubConnectionId");
                        await _spaceHelper.UpdateSpaceUsed(await _helpers.GetSpaceUsed(siteId), spaceHubConnectionId);

                        await _emailHelpers.SendSpaceLowEmail(user.UserName, siteId);
                    }

                    responseViewModel.IsSuccessful = true;
                    responseViewModel.Message = $"Update Operation Successfull";
                    return StatusCode(404, JsonConvert.SerializeObject(responseViewModel));
                }

                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = $"Site with Id {siteId} does not exist";
                return StatusCode(404, JsonConvert.SerializeObject(responseViewModel));
            }
            catch (Exception ex)
            {
                responseViewModel.IsSuccessful = false;
                responseViewModel.Message = ex.Message;
                return StatusCode(404, JsonConvert.SerializeObject(responseViewModel));
            }
        }

        [HttpGet("GetFiles")]
        public IActionResult GetFiles(string filePath)
        {
            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found");
            }

            // Read the file content
            byte[] fileContent = System.IO.File.ReadAllBytes(filePath);

            // Get the content type of file
            const string DefaultContentType = "application/octet-stream";
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out string contentType))
            {
                contentType = DefaultContentType;
            }

            return File(fileContent, contentType);
        }

        [HttpPost("AssignSubAdmin")]
        public async Task<IActionResult> AssignSubAdmin(string Email, string? siteID)
        {
            var responseViewModel = new ResponseViewModel();
            try
            {
                var siteId = Request.Cookies["SiteId"]?.ToString();
                if (siteId == null && siteID != null)
                {
                    siteId = siteID;
                }

                if (siteId != null)
                {
                    var isSiteExist = await _siteServices.CheckIfSiteExist(siteId);
                    if (!isSiteExist)
                    {
                        return StatusCode(404, $"Site with Id {siteId} does not exist");
                    }

                    string email = Email.Trim();

                    if (string.IsNullOrEmpty(email))
                    {
                        return StatusCode(400, $"Email is required");
                    }

                    IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

                    if (email == user.Email)
                    {
                        return StatusCode(204, "Own Email Provided");
                    }

                    var employee = await _employeeService.CheckUserExist(email);
                    if (employee.Email != null)
                    {
                        bool checkRemove = false;
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Contains(Roles.Admin))
                        {
                            responseViewModel.Message = $"User with email {email} has Admin role";
                            responseViewModel.IsSuccessful = false;

                            return StatusCode(400, JsonConvert.SerializeObject(responseViewModel));
                        }

                        var isUserSubAdminBySite = await _userService.IsSubAdminBySiteId(siteId, employee.UserId);
                        if (isUserSubAdminBySite)
                        {
                            responseViewModel.Message = $"User with email {email} is already a SubAdmin";
                            responseViewModel.IsSuccessful = false;

                            return StatusCode(400, JsonConvert.SerializeObject(responseViewModel));
                        }

                        var assignSubAdminResult = await _employeeService.AssignSubAdmin(siteId.Trim(), user.Email, user.UserName, "", user.Id);
                        if ((bool)assignSubAdminResult.IsSuccessful)
                        {
                            foreach (var item in roles)
                            {
                                await _userManager.RemoveFromRoleAsync(user, item);
                                checkRemove = true;
                            }

                            if (checkRemove)
                            {
                                await _userManager.AddToRoleAsync(user, Roles.SubAdmin);

                                responseViewModel.Message = assignSubAdminResult.Message;
                                responseViewModel.IsSuccessful = (bool)assignSubAdminResult.IsSuccessful;
                            }

                            await _emailHelpers.AddedExistingAsSubAdmin(email);

                            return StatusCode(200, JsonConvert.SerializeObject(responseViewModel));
                        } else
                        {
                            responseViewModel.Message = assignSubAdminResult.Message;
                            responseViewModel.IsSuccessful = (bool)assignSubAdminResult.IsSuccessful;

                            return StatusCode(204, JsonConvert.SerializeObject(responseViewModel));
                        }                                                
                    }
                    else
                    {
                        //Send Email to the subadmin
                        var isEmailSent = await _emailHelpers.SendSubAdminAccessPrivilegeInfoEmail(email);
                        if (isEmailSent)
                        {
                            var userApp = new IdentityUser { UserName = employee.Email, Email = employee.Email, LockoutEnabled = false };
                            var findUser = await _userManager.FindByEmailAsync(email);
                            if (findUser != null)
                            {
                                var getRoles = await _userManager.GetRolesAsync(user);
                                foreach (var item in getRoles)
                                {
                                    await _userManager.RemoveFromRoleAsync(findUser, item);
                                }

                                await _userManager.AddToRoleAsync(findUser, Roles.SubAdmin);
                                var isSuccessfullyAssigned = await _siteServices.AssignSubAdminToSite(siteId, user.Id, user.UserName, findUser.Id);

                                responseViewModel.Message = isSuccessfullyAssigned ? "Assigned as SubAdmin success" : $"Assign SubAdmin to Site {siteId} failed";
                                responseViewModel.IsSuccessful = isSuccessfullyAssigned;
                            }
                            else
                            {
                                var isCreateUserSuccess = await _userManager.CreateAsync(userApp, "Nait@1234");
                                if (isCreateUserSuccess.Succeeded)
                                {
                                    var adminUserId = user.Id;
                                    var subAdminUserId = userApp.Id;
                                    var adminUserName = user.UserName;

                                    var addUserToRole = await _userManager.AddToRoleAsync(userApp, Roles.SubAdmin);
                                    var isSuccessfullyAssigned = await _siteServices.AssignSubAdminToSite(siteId, adminUserId, adminUserName, subAdminUserId);

                                    responseViewModel.Message = isSuccessfullyAssigned ? "Assigned as SubAdmin success" : $"Assign SubAdmin to Site {siteId} failed";
                                    responseViewModel.IsSuccessful = isSuccessfullyAssigned;
                                }
                            }

                            return StatusCode(200, JsonConvert.SerializeObject(responseViewModel));
                        }
                    }
                }

                responseViewModel.Message = $"SiteId is Null";
                responseViewModel.IsSuccessful = false;

                return StatusCode(400, JsonConvert.SerializeObject(responseViewModel));

            }
            catch (Exception ex)
            {

                responseViewModel.Message = ex.Message;
                responseViewModel.IsSuccessful = false;

                return StatusCode(500, JsonConvert.SerializeObject(responseViewModel));
            }
        }

        [HttpDelete("DeleteFiles"), Authorize]
        public async Task<IActionResult> DeleteFilesAsync(long fileId, string siteId, long categoryId, ECategory category, EUploadType fileType)
        {
            try
            {
                var filePath = "";
                IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

                if (fileType == EUploadType.Video)
                {
                    filePath = await _setupService.DeleteVideo(fileId, siteId, categoryId, category, user.UserName);
                } else if (fileType == EUploadType.Image)
                {
                    filePath = await _setupService.DeletePhoto(fileId, siteId, categoryId, category, user.UserName);
                } else if (fileType == EUploadType.Pdf)
                {
                    filePath = await _setupService.DeleteFileUpload(fileId, siteId, categoryId, category, user.UserName);
                }

                if(filePath != null  && filePath != "")
                {
                    // Check if the file exists
                    if (System.IO.File.Exists(filePath))
                    {
                        // Delete the file
                        System.IO.File.Delete(filePath);
                    }

                    await _siteSpaceDetailService.DeleteByCategoryDetailId(fileId, siteId, category, fileType);
                    await _spaceHelper.UpdateSpaceUsed(await _helpers.GetSpaceUsed(siteId), _httpContextAccessor.HttpContext.Session.GetString("ProgressHubConnectionId"));

                    return StatusCode(200, "Deleted Successfully");
                } else
                {
                    return StatusCode(204, "File doesn't exist");
                }
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("MultipleDeleteDisaster"), Authorize]
        public async Task<IActionResult> MultipleDeleteDisaster([FromBody]MultipleDisasterDeleteModel multipleDisasterDeleteModel)
        {
            var responseViewModel = new ResponseViewModel();
            try
            {
               var siteId = multipleDisasterDeleteModel.SiteId;
               if (siteId == null)
               {
                    return StatusCode(400, "SiteId is required");
               }

                IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var viewMode = (int)_helpers.GetViewMode(_httpContextAccessor.HttpContext.Session.GetString("ViewMode")); 

                foreach (var disasterId in multipleDisasterDeleteModel.DisasterIds)
                {
                    var result = await _setupService.DeleteDisaster(disasterId, siteId, viewMode, user);
                    if(result.IsSuccessful)
                    {
                        await _siteSpaceDetailService.DeleteByCategoryId(disasterId, siteId, ECategory.Disaster);
                    }
                }

                await _spaceHelper.UpdateSpaceUsed(await _helpers.GetSpaceUsed(siteId), _httpContextAccessor.HttpContext.Session.GetString("ProgressHubConnectionId"));

                responseViewModel.IsSuccessful = true;
                responseViewModel.Message = "Deleted Successfully";
                return StatusCode(200, responseViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}