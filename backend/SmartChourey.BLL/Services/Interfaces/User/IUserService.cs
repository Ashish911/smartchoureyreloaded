using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.SuperAdmin;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface IUserService
    {
        Task<UserDetailViewModel> CheckUserExistAsync(string email);
        Task<bool> IsSubAdminBySiteId(string siteId, string userId);
        Task<List<UserInformationViewModel>> GetUserList();
        Task<UserInformationViewModel> CreateUser(UserInformationViewModel userInformationViewModel);
    }
}
