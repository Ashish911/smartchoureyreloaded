using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.ViewModel.Admin
{
    public class UserDetailViewModel: AdminStatusViewModel
    {
        public long EmployeeId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? FullAddress { get; set; }
        public string? MobileNumber { get; set; }
        public string? UserId { get; set; }

        public DateTime LoggedInDate { get; set; }
        public string? SiteId { get; set; }
        public string? SiteName { get; set; }


    }
}
