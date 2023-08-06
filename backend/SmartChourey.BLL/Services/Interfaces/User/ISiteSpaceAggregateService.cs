using SmartChourey.BLL.Dtos;
using SmartChourey.BLL.ViewModel.SuperAdmin;
using SmartChourey.DAL;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface ISiteSpaceAggregateService
    {
        bool InsertAndUpdate(SiteSpaceAggregate siteSpaceAggregate);
        Task<SiteSpaceAggregateDto> InsertAndUpdateSpaceAllocation(string siteId, int newAllocatedSpace);
        Task<SiteSpaceAggregate?> GetAggregateSpace(string siteId);
        Task<IEnumerable<SiteSpaceAggregateViewModel>> GetAggregateSpaces();
    }
}
