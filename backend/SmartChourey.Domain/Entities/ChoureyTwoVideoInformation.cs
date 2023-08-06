using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class ChoureyTwoVideoInformation
{
    public long VideoId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public string SiteId { get; set; } = null!;

    public long ChoureyTwoId { get; set; }

    public string? CustomName { get; set; }

    public string? ThumbFileName { get; set; }
}
