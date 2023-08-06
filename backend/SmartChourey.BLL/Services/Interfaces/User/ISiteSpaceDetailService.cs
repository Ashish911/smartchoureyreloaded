using BusinessLogicLayer.Configuration;
using SmartChourey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface ISiteSpaceDetailService
    {
        Task<bool> Insert(SiteSpaceDetail data);
        Task<bool> DeleteByCategoryDetailId(long id, string siteId, EnumHelpers.ECategory category, EnumHelpers.EUploadType uploadType);
        Task<bool> DeleteByCategoryId(long id, string siteId, EnumHelpers.ECategory category);
        Task<bool> DeleteBySiteSpaceDetailId(long id, string siteId);
    }
}
