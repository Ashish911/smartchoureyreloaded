using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class SiteInformation
{
    public string Id { get; set; } = null!;

    public string SiteName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool? IsTrial { get; set; }

    public DateTime? TrialExpireOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? PeriodStart { get; set; }

    public string? PeriodEnd { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? BrowseTimeFrom { get; set; }

    public string? BrowseTimeTo { get; set; }

    public int? Gpsrange { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? ActivateOn { get; set; }

    public string QrCodeValue { get; set; } = null!;

    public virtual ICollection<AccidentGalleryInformation> AccidentGalleryInformations { get; } = new List<AccidentGalleryInformation>();

    public virtual ICollection<AccidentPhotoInformation> AccidentPhotoInformations { get; } = new List<AccidentPhotoInformation>();

    public virtual ICollection<NewsGalleryInformation> NewsGalleryInformations { get; } = new List<NewsGalleryInformation>();

    public virtual ICollection<NewsPhotoInformation> NewsPhotoInformations { get; } = new List<NewsPhotoInformation>();

    public virtual ICollection<NoticeGalleryInformation> NoticeGalleryInformations { get; } = new List<NoticeGalleryInformation>();

    public virtual ICollection<NoticePhotoInformation> NoticePhotoInformations { get; } = new List<NoticePhotoInformation>();

    public virtual ICollection<QrcodeInformation> QrcodeInformations { get; } = new List<QrcodeInformation>();

    public virtual ICollection<SafetyGalleryInformation> SafetyGalleryInformations { get; } = new List<SafetyGalleryInformation>();

    public virtual ICollection<SafetyPhotoInformation> SafetyPhotoInformations { get; } = new List<SafetyPhotoInformation>();

    public virtual ICollection<SiteAccidentInformation> SiteAccidentInformations { get; } = new List<SiteAccidentInformation>();

    public virtual ICollection<SiteNewsInformation> SiteNewsInformations { get; } = new List<SiteNewsInformation>();

    public virtual ICollection<SiteNoticeInformation> SiteNoticeInformations { get; } = new List<SiteNoticeInformation>();

    public virtual ICollection<SiteSafetyInformation> SiteSafetyInformations { get; } = new List<SiteSafetyInformation>();

    public virtual ICollection<SiteWorkInformation> SiteWorkInformations { get; } = new List<SiteWorkInformation>();

    public virtual ICollection<UserSiteInformation> UserSiteInformations { get; } = new List<UserSiteInformation>();
}
