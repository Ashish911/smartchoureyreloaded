using System.ComponentModel.DataAnnotations;

namespace SmartChourey.WebAPI.Models
{
    public class UserboardDashboardViewModel
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
        public string? CurrentUserId { get; set; }
        public string? UserName { get; set; }
    }
}
