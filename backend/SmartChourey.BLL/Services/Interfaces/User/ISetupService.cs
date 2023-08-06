using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SmartChourey.BLL.ViewModel;
using SmartChourey.BLL.ViewModel.Admin;
using static BusinessLogicLayer.Configuration.EnumHelpers;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface ISetupService
    {
        Task<List<ChoureyOneViewModel>> ListChoureyOne(string siteId, int viewMode);
        Task<ChoureyOneViewModel> ChoureyOneDetails(long choureyOneId, string siteId, int viewMode);
        Task<List<ChoureyTwoViewModel>> ListChoureyTwo(string siteId, int viewMode);
        Task<ChoureyTwoViewModel> ChoureyTwoDetails(long choureyTwoId, string siteId, int viewMode);
        Task<ResponseViewModel> DeleteChourey(ChoureyDeleteModel model);
        Task<List<DisasterViewModel>> ListDisaster(string siteId, int viewMode);
        Task<DisasterViewModel> GetDisasterDetailsById(long disasterId, string siteId, int viewMode);

        Task<ChoureyOneViewModel> InsertUpdateChoureyOneAsync(ChoureyOneViewModel choureyOneViewModel);
        Task<float> ProcessUploads(ChoureyOneViewModel choureyOneViewModel, IList<IFormFile> uploads, float addedSpace, IList<Task> uploadTasks, string rootPath);
        Task<float> ProcessFiles(ChoureyOneViewModel choureyOneViewModel, IList<IFormFile> files, float addedSpace, IList<Task> uploadTasks, string rootPath);
        Task<float> ProcessVideos(ChoureyOneViewModel choureyOneViewModel, IList<IFormFile> videos, IList<IFormFile> thumbs, float addedSpace, IList<Task> uploadTasks, string rootPath);
        Task UpdateChoureyOneVideoCustomName(string customName, long videoId, string userName);
        Task UpdateChoureyOnePhotoCustomName(string customName, long photoId, string userName);
        Task UpdateSiteSpaceAggregate(string siteId, float addedSpace);

        Task<ChoureyTwoViewModel> InsertUpdateChoureyTwoAsync(ChoureyTwoViewModel choureyTwoViewModel);
        Task<float> ProcessUploads(ChoureyTwoViewModel choureyTwoViewModel, IList<IFormFile> uploads, float addedSpace, IList<Task> uploadTasks, string rootPath);
        Task<float> ProcessFiles(ChoureyTwoViewModel choureyTwoViewModel, IList<IFormFile> files, float addedSpace, IList<Task> uploadTasks, string rootPath);
        Task<float> ProcessVideos(ChoureyTwoViewModel choureyTwoViewModel, IList<IFormFile> videos, IList<IFormFile> thumbs, float addedSpace, IList<Task> uploadTasks, string rootPath);
        Task UpdateChoureyTwoVideoCustomName(string customName, long videoId, string userName);
        Task UpdateChoureyTwoPhotoCustomName(string customName, long photoId, string userName);

        Task<DisasterViewModel> InsertUpdateDisaster(DisasterViewModel disasterViewModel);
        Task<float> ProcessUploads(DisasterViewModel disasterViewModel, IList<IFormFile> uploads, float addedSpace, IList<Task> uploadTasks, string rootPath);
        Task<float> ProcessFiles(DisasterViewModel disasterViewModel, IList<IFormFile> files, float addedSpace, IList<Task> uploadTasks, string rootPath);
        Task UpdateDisasterPhotoCustomName(string customName, long photoId, string userName);
        Task<ResponseViewModel> DeleteDisaster(long disasterId, string siteId, int viewMode, IdentityUser user);

        Task<string> DeleteVideo(long videoId, string siteId, long categoryId, ECategory category, string userName);
        Task<string> DeletePhoto(long photoId, string siteId, long categoryId, ECategory category, string userName);
        Task<string> DeleteFileUpload(long fileUploadId, string siteId, long categoryId, ECategory category, string userName);

    }
}
