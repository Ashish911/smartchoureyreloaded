using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AuditSiteNewsInformation
{
    public long AuditNewsId { get; set; }

    public long NewsId { get; set; }

    public string SiteId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string ActionDone { get; set; } = null!;
}
