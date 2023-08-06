using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.ViewModel.SuperAdmin
{
    public class SiteSpaceAggregateViewModel
    {
        public long SiteSpaceUsedId { get; set; }
        public string? SiteId { get; set; }
        public string? SiteName { get; set; }
        public int AllocatedSpace { get; set; }
    }
}
