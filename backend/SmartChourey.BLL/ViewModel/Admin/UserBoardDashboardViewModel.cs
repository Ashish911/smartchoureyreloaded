using SmartChourey.BLL.ViewModel.Admin;
using SmartChourey.DAL;

namespace SmartChourey.BLL.ViewModel.Userboard
{
    public class UserBoardDashboardViewModel
    {
        public UserBoardDashboardViewModel()
        {
            this.choureyOneModelList = new List<ChoureyOneViewModel>();
            this.choureyTwoModelList = new List<ChoureyTwoViewModel>();
            this.disasterModelList = new List<DisasterViewModel>();
        }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public bool IsSiteActive { get; set; }
        public string SiteAddress { get; set; }
        public string SiteTime { get; set; }
        public string ProjectTimeLine { get; set; }
        public bool isEditable { get; set; }

        public long? SiteDeclarationId { get; set; }
        public string SiteDeclarationTitle { get; set; }
        public string SiteDeclarationDescription { get; set; }

        public bool? userDeclarationChecked { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UserSiteDeclarationId { get; set; }
        public SiteDeclarationViewModel SiteDeclarationList { get; set; }

        public List<ChoureyOneViewModel> choureyOneModelList { get; set; }
        public List<ChoureyTwoViewModel> choureyTwoModelList { get; set; }
        public List<DisasterViewModel> disasterModelList { get; set; }
        public IList<FileUpload> fileUploads { get; set; }

    }
    public class UserBoardChoureyDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public long? GalleryId { get; set; }
        public long? VideoId { get; set; }
        public string VideoFileName { get; set; }
        public string VideoCustomName { get; set; }
        public long? PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string PhotoFilePath { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoCustomName { get; set; }
        public string SiteId { get; set; }
    }

    public class ChoureyOneViewModel
    {
        public ChoureyOneViewModel()
        {
            this.PhotoListModel = new List<PhotoInfoViewModel>();
            this.videoListModel = new List<VideoInfoViewModel>();

            this.choureyOnePositionList = new List<string>();
        }
        public List<PhotoInfoViewModel> PhotoListModel { get; set; }
        public List<VideoInfoViewModel> videoListModel { get; set; }
        public long? ChoureyOneId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public List<string> choureyOnePositionList { get; set; }
        public long? GalleryId { get; set; }
        public long? VideoId { get; set; }
        public string SiteId { get; set; }
        public int BrowseRange { get; set; }
    }
    public class ChoureyTwoViewModel
    {
        public ChoureyTwoViewModel()
        {
            this.PhotoListModel = new List<PhotoInfoViewModel>();
            this.videoListModel = new List<VideoInfoViewModel>();
            this.choureyTwoPositionList = new List<string>();
        }
        public long? ChoureyTwoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public long? GalleryId { get; set; }
        public long? VideoId { get; set; }
        public string SiteId { get; set; }
        public int BrowseRange { get; set; }
        public List<PhotoInfoViewModel> PhotoListModel { get; set; }
        public List<VideoInfoViewModel> videoListModel { get; set; }
        public List<string> choureyTwoPositionList { get; set; }
    }
    public class DisasterViewModel
    {
        public DisasterViewModel()
        {
            this.PhotoListModel = new List<PhotoInfoViewModel>();
            this.disasterPositionList = new List<string>();
        }
        public long? DisasterId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public long? GalleryId { get; set; }
        public long? VideoId { get; set; }
        public string SiteId { get; set; }
        public int BrowseRange { get; set; }
        public List<PhotoInfoViewModel> PhotoListModel { get; set; }
        public List<string> disasterPositionList { get; set; }
    }

    public class PhotoInfoViewModel
    {
        public long? GalleryId { get; set; }
        public long? PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string CustomName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
    public class VideoInfoViewModel
    {
        public long? VideoId { get; set; }
        public long? choureyId { get; set; }
        public string FileName { get; set; }
        public string CustomName { get; set; }
    }

}
