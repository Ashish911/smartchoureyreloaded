using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AuditDisasterVideoInformation
{
    public long AuditVideoId { get; set; }

    public long VideoId { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public string SiteId { get; set; } = null!;

    public long DisasterId { get; set; }

    public string ActionDone { get; set; } = null!;
}
