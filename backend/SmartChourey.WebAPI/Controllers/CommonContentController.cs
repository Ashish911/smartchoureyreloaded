using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.BLL.Services.User;
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.WebAPI.Hubs;
using static BusinessLogicLayer.Configuration.EnumHelpers;

namespace SmartChourey.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonContentController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPublicUserBoardService _publicUserBoardService;
        private readonly IHostEnvironment _hostEnvironment;

        public CommonContentController(
            UserManager<IdentityUser> userManager,
            IPublicUserBoardService publicUserBoardService,
            IHostEnvironment hostEnvironment
            )
        {
            _publicUserBoardService = publicUserBoardService;
            _userManager= userManager;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("ListPublicUserBoard")]
        public async Task<List<PublicUserBoardViewModel>> ListAll()
        {
            var result = await _publicUserBoardService.ListAll();

            return result;
        }

        [HttpPost("InsertOrUpdatePublicUserBoard"), Authorize]
        public async Task<PublicUserBoardViewModel> InsertUpdate([FromForm] PublicUserBoardViewModel publicUserBoardViewModel, IList<IFormFile> files)
        {
            try
            {
                IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

                publicUserBoardViewModel.CurrentUserId = user.Id;
                publicUserBoardViewModel.UserName = user.UserName;

                var insertResult = await _publicUserBoardService.InsertUpdate(publicUserBoardViewModel);

                if ((bool)insertResult.IsSuccessful)
                {
                    var rootPath = _hostEnvironment.ContentRootPath;
                    await _publicUserBoardService.ProcessFiles(insertResult, files, rootPath);
                }
                return publicUserBoardViewModel;
            } catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetCommonContentDetail"), Authorize]
        public async Task<IActionResult> Detail(long id)
        {
            var publicUserBoardViewModel = new PublicUserBoardViewModel();

            publicUserBoardViewModel = _publicUserBoardService.ListAll().Result.Where(x => x.PublicUserboardId == id).FirstOrDefault();

            var rootPath = _hostEnvironment.ContentRootPath;
            if(publicUserBoardViewModel != null)
            {
                var subPath = "PublicImage/User/" + publicUserBoardViewModel.PublicUserboardId;
                var exists = Directory.Exists(Path.Combine(rootPath, subPath));

                if (exists)
                {
                    var imageFiles = Directory.GetFiles(Path.Combine(rootPath, subPath));
                    if (imageFiles.Count() != 0)
                    {
                        foreach (var item in imageFiles)
                        {
                            publicUserBoardViewModel.ImageList.Add(Path.GetFileName(item));
                        }
                    }
                }

                return StatusCode(200, publicUserBoardViewModel);
            }

            return StatusCode(404, "PublicUserBoard Not Found");
        }

        [HttpDelete("DeletPublicUserBoard"), Authorize]
        public async Task<IActionResult> Delete(int publicUserboardId)
        {
            try
            {
                var isSuccess = await _publicUserBoardService.Delete(publicUserboardId);

                if (isSuccess)
                {
                    return StatusCode(200, "Deleted Successfully");
                }
                else
                {
                    return StatusCode(400, "Delete Failed");
                }
            } 
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteFile"), Authorize]
        public async Task<IActionResult> DeleteFile(long photoId, int publicUserboardId)
        {
            try
            {
                var filePath = "";
                filePath = await _publicUserBoardService.DeletePhoto(publicUserboardId, photoId);

                if (filePath != null && filePath != "")
                {
                    // Check if the file exists
                    if (System.IO.File.Exists(filePath))
                    {
                        // Delete the file
                        System.IO.File.Delete(filePath);
                    }

                    return StatusCode(200, "Deleted Successfully");
                }
                else
                {
                    return StatusCode(204, "File doesn't exist");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
