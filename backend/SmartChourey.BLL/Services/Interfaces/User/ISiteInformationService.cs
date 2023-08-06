using SmartChourey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface ISiteInformationService
    {
        Task<string> GetSiteName(string siteId);
        Task<SiteInformation> GetSiteById(string siteId);
    }
}
