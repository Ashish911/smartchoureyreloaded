using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class DeviceLog
{
    public long DeviceLogId { get; set; }

    public string FkDeviceUniqueId { get; set; } = null!;

    public DateTime? CreatedOn { get; set; }
}
