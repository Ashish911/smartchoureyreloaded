
using Microsoft.AspNetCore.Http;
using SmartChourey.BLL.ViewModel.SuperAdmin;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface IPublicUserBoardService
    {
        Task<List<PublicUserBoardViewModel>> ListAll();
        Task<PublicUserBoardViewModel> InsertUpdate(PublicUserBoardViewModel model);
        Task<PublicUserBoardViewModel> ProcessFiles(PublicUserBoardViewModel publicUserBoardViewModel, IList<IFormFile> files, string rootPath);
        Task<bool> Delete(int publicUserboardId);
        Task<PublicUserBoardViewModel> InsertPhoto(PublicUserBoardViewModel model);
        Task<string> DeletePhoto(int publicUserboardId, long photoId);

    }
}
