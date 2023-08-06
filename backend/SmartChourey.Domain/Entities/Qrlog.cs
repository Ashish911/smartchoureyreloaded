using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class Qrlog
{
    public long QrlogId { get; set; }

    public string UserId { get; set; } = null!;

    public string SiteId { get; set; } = null!;

    public DateTime EntryTime { get; set; }
}
