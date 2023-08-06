using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class DeviceSiteLog
{
    public long DeviceSiteLogId { get; set; }

    public string FkDeviceUniqueId { get; set; } = null!;

    public string? FkSiteId { get; set; }

    public DateTime? CreatedOn { get; set; }
}
