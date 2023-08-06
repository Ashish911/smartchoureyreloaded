using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.ViewModel.SuperAdmin
{
    public class SiteCodeAccessViewModel
    {
        public long SiteCodeId { get; set; }
        public string? SiteCode { get; set; }
        public string? SiteId { get; set; }
        public string? UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsOccupied { get; set; }

        public string? SiteName { get; set; }
        public string? UserName { get; set; }
        public string? CurrentUserId { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
