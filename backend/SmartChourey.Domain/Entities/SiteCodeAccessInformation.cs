using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class SiteCodeAccessInformation
{
    public long SiteCodeId { get; set; }

    public string SiteCode { get; set; } = null!;

    public string? SiteId { get; set; }

    public string? UserId { get; set; }

    public bool IsActive { get; set; }

    public bool IsOccupied { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? OccupiedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }
}
