using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.ViewModel.SuperAdmin
{
    public class PublicUserBoardViewModel
    {
        public PublicUserBoardViewModel()
        {
            this.ImageList = new List<string>();
        }

        public int? PublicUserboardId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? UpdatedBy { get; set; }
        public long? PhotoId { get; set; }
        public string? PhotoName { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public List<string>? ImageList { get; set; }
        public string? Message { get; set; }
        public string? CurrentUserId { get; set; }
        public bool? IsSuccessful { get; set; }
        public string? UserName { get; set; }
    }
}
