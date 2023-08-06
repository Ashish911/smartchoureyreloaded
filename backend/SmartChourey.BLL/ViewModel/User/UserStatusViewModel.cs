using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.ViewModel.User
{
    public class UserStatusViewModel
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public string? CurrentUserId { get; set; }
        public string? UserName { get; set; }
        public string? UserRole { get; set; }
    }
}
