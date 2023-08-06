using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AuditSafetyPhotoInformation
{
    public long AuditPhotoId { get; set; }

    public long PhotoId { get; set; }

    public string PhotoName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public int? GalleryId { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public string SiteId { get; set; } = null!;

    public long SafetyId { get; set; }

    public string? ActionDone { get; set; }
}
