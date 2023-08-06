using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class ChangeLog
{
    public string ChangeLogId { get; set; } = null!;

    public int? Ecategory { get; set; }

    public int? EchangeCategory { get; set; }

    public int? EchangeType { get; set; }

    public string? SiteId { get; set; }

    public string? Id { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ChangeCount { get; set; }

    public string? ChangedProperties { get; set; }
}
