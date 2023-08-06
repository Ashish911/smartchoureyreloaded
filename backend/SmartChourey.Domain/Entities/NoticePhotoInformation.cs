using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class NoticePhotoInformation
{
    public long PhotoId { get; set; }

    public string PhotoName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public long? GalleryId { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public string SiteId { get; set; } = null!;

    public long NoticeId { get; set; }

    public virtual NoticeGalleryInformation? Gallery { get; set; }

    public virtual SiteNoticeInformation Notice { get; set; } = null!;

    public virtual SiteInformation Site { get; set; } = null!;
}
