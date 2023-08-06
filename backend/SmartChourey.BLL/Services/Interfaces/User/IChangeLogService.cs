using BusinessLogicLayer.Configuration;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartChourey.BLL.Dtos;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface IChangeLogService<T> where T : class
    {
        Task<bool> Update(EntityEntry<T> entry, long id
            , EnumHelpers.EChangeType changeType, EnumHelpers.ECategory category, EnumHelpers.EChangeCategory changeCategory
            , string createdBy, string siteId);
        Task<bool> Insert(long id
           , EnumHelpers.EChangeType changeType, EnumHelpers.ECategory category, EnumHelpers.EChangeCategory changeCategory
           , string createdBy, string siteId);

        Task<IList<ChangeLogDto>> GetAsync(string siteId, string fromDate, string toDate);
    }
}
