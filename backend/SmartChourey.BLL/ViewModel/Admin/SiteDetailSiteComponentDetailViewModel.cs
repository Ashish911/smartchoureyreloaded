using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.ViewModel.Admin
{
    public class SiteDetailSiteComponentDetailViewModel: AdminStatusViewModel
    {
        public SiteDetailSiteComponentDetailViewModel()
        {
            this.ImageList = new List<string>();
        }

        public string? Id { get; set; }
        public string? SiteName { get; set; }
        public bool SiteActive { get; set; }
        public string? PeriodStart { get; set; }
        public string? PeriodEnd { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? BrowseTimeFrom { get; set; }
        public string? BrowzeTimeFrom_Hr { get; set; }
        public string? BrowzeTimeFrom_Min { get; set; }
        public string? BrowzeTimeFrom_AMPM { get; set; }
        public string? BrowzeTimeTo_Hr { get; set; }
        public string? BrowzeTimeTo_Min { get; set; }
        public string? BrowzeTimeTo_AMPM { get; set; }

        public string? SiteAccessCode { get; set; }

        public string? BrowseTimeTo { get; set; }
        public int? GPSRange { get; set; }
        public string? QRCodeValue { get; set; }
        public long QRId { get; set; }
        public bool IsActive { get; set; }

        public long AccidentId { get; set; }
        public string? AccidentTitle { get; set; }
        public string? AccidentDescription { get; set; }
        public DateTime AccidentCreatedDate { get; set; }
        public long AccidentGalleryId { get; set; }
        public string? AccidentGalleryName { get; set; }
        public DateTime AccidentGalleryCreatedDate { get; set; }
        public long AccidentPhotoId { get; set; }
        public string? AccidentPhotoName { get; set; }
        public string? AccidentFileName { get; set; }
        public string? AccidentFilePath { get; set; }
        public DateTime AccidentPhotoCreatedOn { get; set; }
        public bool IsSiteAccidentActive { get; set; }


        public long SafetyId { get; set; }
        public string? SafetyTitle { get; set; }
        public string? SafetyDescription { get; set; }
        public DateTime SafetyCreatedDate { get; set; }
        public long SafetyGalleryId { get; set; }
        public string? SafetyGalleryName { get; set; }
        public DateTime SafetyGalleryCreatedDate { get; set; }
        public long SafetyPhotoId { get; set; }
        public string? SafetyPhotoName { get; set; }
        public string? SafetyFileName { get; set; }
        public string? SafetyFilePath { get; set; }
        public DateTime SafetyPhotoCreatedOn { get; set; }
        public bool? SafetyDaily { get; set; }
        public bool? SafetyWeekly { get; set; }
        public bool? SafetyMonthly { get; set; }
        public bool IsSiteSafetyActive { get; set; }

        public long NoticeId { get; set; }
        public string? NoticeTitle { get; set; }
        public string? NoticeDescription { get; set; }
        public DateTime NoticeCreatedDate { get; set; }
        public long NoticeGalleryId { get; set; }
        public string? NoticeGalleryName { get; set; }
        public DateTime NoticeGalleryCreatedDate { get; set; }
        public long NoticePhotoId { get; set; }
        public string? NoticePhotoName { get; set; }
        public string? NoticeFileName { get; set; }
        public string? NoticeFilePath { get; set; }
        public DateTime NoticePhotoCreatedOn { get; set; }
        public bool IsSiteNoticeActive { get; set; }


        public long NewsId { get; set; }
        public string? NewsTitle { get; set; }
        public string? NewsDescription { get; set; }
        public DateTime NewsCreatedDate { get; set; }
        public long NewsGalleryId { get; set; }
        public string? NewsGalleryName { get; set; }
        public DateTime NewsGalleryCreatedDate { get; set; }
        public long NewsPhotoId { get; set; }
        public string? NewsPhotoName { get; set; }
        public string? NewsFileName { get; set; }
        public string? NewsFilePath { get; set; }
        public DateTime NewsPhotoCreatedOn { get; set; }
        public bool IsSiteNewsActive { get; set; }

        public long WorkId { get; set; }
        public string? WorkTitle { get; set; }
        public string? WorkDescription { get; set; }
        public bool IsSiteWorkActive { get; set; }
        public DateTime WorkCreatedOn { get; set; }

        public List<string?> ImageList { get; set; }

        public long VideoId { get; set; }
        public string? VideoTitle { get; set; }
        public string? VideoMessage { get; set; }
        public string? VideoFileName { get; set; }
        public string? VideoFilePath { get; set; }
        public DateTime VideoCreatedOn { get; set; }
        public bool IsVideoActive { get; set; }

        public string? Longitude { get; set; }

        public string? Latitude { get; set; }
        public bool IsSetLocation { get; set; }
    }
}
