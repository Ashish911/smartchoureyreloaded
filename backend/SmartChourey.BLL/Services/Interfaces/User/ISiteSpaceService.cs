using SmartChourey.BLL.Dtos;
using SmartChourey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface ISiteSpaceService
    {
        bool InsertAndUpdate(SiteSpaceAggregate data);
        bool InsertAndUpdateSpaceAllocation(string siteId, int newAllocatedSpace);
        SiteSpaceAggregate GetAggregateSpace(string siteId);
        IList<SiteSpaceAggregateDto> GetAggregateSpaces();
    }
}
