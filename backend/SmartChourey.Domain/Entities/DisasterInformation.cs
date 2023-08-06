using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class DisasterInformation
{
    public long DisasterId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string SiteId { get; set; } = null!;

    public int Position { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public int ViewMode { get; set; }

    public int? BrowseRange { get; set; }
}
