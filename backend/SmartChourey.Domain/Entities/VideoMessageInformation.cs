using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class VideoMessageInformation
{
    public long VideoId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? FilePath { get; set; }

    public string? FileName { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string SiteId { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }
}
