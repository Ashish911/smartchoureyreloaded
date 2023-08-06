using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AuditNoticeGalleryInformation
{
    public long AuditGalleryId { get; set; }

    public long GalleryId { get; set; }

    public string GalleyName { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public string SiteId { get; set; } = null!;

    public long NoticeId { get; set; }

    public string? ActionDone { get; set; }
}
