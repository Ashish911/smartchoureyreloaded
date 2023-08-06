using SmartChourey.BLL.ViewModel.Userboard;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface IUserboardService
    {
        Task<UserBoardDashboardViewModel> GetUserBoard(string siteId, string userId, int viewMode);
    }
}
