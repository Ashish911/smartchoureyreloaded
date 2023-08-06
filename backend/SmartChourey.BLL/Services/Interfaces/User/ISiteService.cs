using SmartChourey.BLL.ViewModel;
using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.User;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface ISiteService
    {
        Task<ResponseViewModel> InsertAsync(SiteCreateViewModel model);
        Task<List<SiteDetailSiteComponentDetailViewModel>> GetSitesByAdmin(string userId, string roleName);
        Task<SiteDetailSiteComponentDetailViewModel> GetSitesDetails(string siteId);
        Task<SiteImplementationViewModel> SiteDetailByQRCodeAsync(string QRCode, string UserId, string UserName, string latitude, string longitude);
        Task<bool> CheckIfSiteExist(string siteId);
        Task<SiteDetailSiteComponentDetailViewModel> UpdateSite(SiteDetailSiteComponentDetailViewModel model);
        Task<bool> AssignSubAdminToSite(string siteId, string AdminUserId, string AdminUserName, string SubAdminUserId);

        //EmployeeDetailViewModel Update(EmployeeDetailViewModel model);
        //EmployeeDetailViewModel GetProfile(string UserId);
    }
}
