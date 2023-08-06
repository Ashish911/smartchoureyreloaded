using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.BLL.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface ISiteCodeService
    {
        Task<SiteCodeAccessViewModel> GenerateSiteCodeAccessAsync(SiteCodeAccessViewModel model);
        Task<bool> AccessSiteCreation(string code);
        Task<List<SiteCodeAccessViewModel>> ListSiteCodes();
        Task<bool> DeleteSiteCode(long siteCodeId);
    }
}
