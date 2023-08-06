using SmartChourey.BLL.ViewModel;
using SmartChourey.BLL.ViewModel.Admin;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface ISubAdminService
    {
        Task<List<UserDetailViewModel>> ListSubAdminBySiteId(string siteId);
        Task<bool> DeleteSiteSubAdmin(long id);
    }
}
