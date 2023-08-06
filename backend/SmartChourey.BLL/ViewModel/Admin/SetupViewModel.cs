using BusinessLogicLayer.Configuration;
using SmartChourey.DAL;

namespace SmartChourey.BLL.ViewModel.Admin
{
    public class ChoureyOneViewModel : AdminStatusViewModel
    {
        public ChoureyOneViewModel()
        {
            this.ImageList = new List<PhotoModel>();
            this.VideoList = new List<VideoModel>();
            this.FileList = new List<FileUpload>();
            this.videoModel = new VideoModel();
            this.photoModel = new PhotoModel();
        }
        public long? ChoureyOneId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string SiteId { get; set; }
        public string? SiteName { get; set; }
        public int? Position { get; set; }
        public bool IsActive { get; set; }

        public int? BrowseRange { get; set; }
        public DateTime? CreatedOn { get; set; }

        public long? GalleryId { get; set; }
        public string? GalleyName { get; set; }
        public long? PhotoId { get; set; }
        public string? PhotoName { get; set; }
        public string? FileName { get; set; }
        public string? CustomName { get; set; }
        public string? FilePath { get; set; }
        public List<PhotoModel>? ImageList { get; set; }

        public long? VideoId { get; set; }
        public string? VideoFileName { get; set; }
        public string? VideoFilePath { get; set; }
        public string? VideoCustomName { get; set; }
        public string? ThumbFileName { get; set; }
        public List<VideoModel>? VideoList { get; set; }
        public IList<FileUpload>? FileList { get; set; }
        public PhotoModel? photoModel { get; set; }
        public VideoModel? videoModel { get; set; }
        public EnumHelpers.EViewMode? EViewMode { get; set; }
    }
    public class ChoureyTwoViewModel : AdminStatusViewModel
    {
        public ChoureyTwoViewModel()
        {
            this.ImageList = new List<PhotoModel>();
            this.VideoList = new List<VideoModel>();
            this.FileList = new List<FileUpload>();
            this.videoModel = new VideoModel();
            this.photoModel = new PhotoModel();
        }
        public long? ChoureyTwoId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string SiteId { get; set; }
        public string? SiteName { get; set; }
        public int? Position { get; set; }
        public bool IsActive { get; set; }
        public int? BrowseRange { get; set; }
        public DateTime? CreatedOn { get; set; }

        public long? GalleryId { get; set; }
        public string? GalleyName { get; set; }
        public long? PhotoId { get; set; }
        public string? PhotoName { get; set; }
        public string? FileName { get; set; }
        public string? CustomName { get; set; }
        public string? FilePath { get; set; }
        public List<PhotoModel>? ImageList { get; set; }

        public long? VideoId { get; set; }
        public string? VideoFileName { get; set; }
        public string? VideoCustomName { get; set; }
        public string? ThumbFileName { get; set; }
        public string? VideoFilePath { get; set; }
        public List<VideoModel>? VideoList { get; set; }
        public IList<FileUpload>? FileList { get; set; }
        public PhotoModel? photoModel { get; set; }
        public VideoModel? videoModel { get; set; }
        public EnumHelpers.EViewMode? EViewMode { get; set; }
    }
    public partial class SiteDeclarationViewModel : AdminStatusViewModel
    {

        public SiteDeclarationViewModel()
        {
            this.DescriptionList = new List<SiteDeclarationDescription>();
            this.DescriptionModel = new SiteDeclarationDescription();
        }
        public List<SiteDeclarationDescription> DescriptionList { get; set; }
        public long SiteDeclarationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public bool IsActive { get; set; }

        public SiteDeclarationDescription DescriptionModel { get; set; }
        public EnumHelpers.EViewMode EViewMode { get; set; }
    }
    public class SiteDeclarationDescription
    {
        public string Description { get; set; }
    }
    public class PhotoModel
    {
        public long? PhotoId { get; set; }
        public string? PhotoName { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? CustomName { get; set; }
        public IList<ChoureyMediaGetDto> Comments { get; set; }
        public PhotoModel()
        {
            Comments = new List<ChoureyMediaGetDto>();
        }
    }
    public class VideoModel
    {
        public long? VideoId { get; set; }
        public string? VideoFileName { get; set; }
        public string? VideoFilePath { get; set; }
        public string? CustomName { get; set; }
        public string? ThumbFileName { get; set; }
        public IList<ChoureyMediaGetDto> Comments { get; set; }
        public VideoModel()
        {
            Comments = new List<ChoureyMediaGetDto>();
        }
    }
    public class ChoureyMediaGetDto
    {
        public string Comment { get; set; }
        public long ChoureyMediaCommentId { get; set; }
        public long ChoureyMediaId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int EUploadType { get; set; }
        public bool IsDelete { get; set; }
    }
    public class ChoureyDeleteModel
    {
        public long ChoureyId { get; set; }
        public string SiteId { get; set; }
        public bool IsChoureyOne { get; set; }
    }
    public class MultipleChoureyDeleteModel
    {
        public List<long> ChoureyIds { get; set; }
        public string SiteId { get; set; }
        public bool IsChoureyOne { get; set; }
    }
    public class MultipleDisasterDeleteModel
    {
        public List<long> DisasterIds { get; set; }
        public string SiteId { get; set; }
    }
    public class DisasterViewModel : AdminStatusViewModel
    {
        public DisasterViewModel()
        {
            this.ImageList = new List<PhotoModel>();
            this.VideoList = new List<VideoModel>();
            this.FileList = new List<FileUpload>();
            this.videoModel = new VideoModel();
            this.photoModel = new PhotoModel();
        }
        public PhotoModel? photoModel { get; set; }
        public VideoModel? videoModel { get; set; }
        public long? DisasterId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string SiteId { get; set; }
        public string? SiteName { get; set; }
        public int? Position { get; set; }
        public bool IsActive { get; set; }
        public int? BrowseRange { get; set; }
        public DateTime? CreatedOn { get; set; }

        public long? GalleryId { get; set; }
        public string? GalleyName { get; set; }
        public long? PhotoId { get; set; }
        public string? PhotoName { get; set; }
        public string? FileName { get; set; }
        public string? CustomName { get; set; }
        public string? FilePath { get; set; }
        public List<PhotoModel>? ImageList { get; set; }

        public long? VideoId { get; set; }
        public string? VideoFileName { get; set; }
        public string? ThumbFileName { get; set; }
        public string? VideoCustomName { get; set; }
        public string? VideoFilePath { get; set; }
        public List<VideoModel>? VideoList { get; set; }
        public IList<FileUpload>? FileList { get; set; }
        public EnumHelpers.EViewMode? EViewMode { get; set; }
    }
    
}
