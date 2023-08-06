using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.BLL.ViewModel.User;
using SmartChourey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface ISiteDeclarationService
    {
        Task<List<SiteDeclarationViewModel>> GetSiteDeclarationBySiteIdAsync(string siteId, int viewMode);
        Task<SiteDeclarationViewModel> InsertUpdateDeclaration(SiteDeclarationViewModel model);
        Task<SiteDeclarationViewModel> DeleteSiteDeclaration(long siteDeclarationId, string siteId);
        Task<SiteDeclarationViewModel> GetSiteDeclarationDetailsById(long siteDeclarationId, string siteId);
    }
}
