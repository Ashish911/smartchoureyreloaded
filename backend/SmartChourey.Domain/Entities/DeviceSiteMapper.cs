using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class DeviceSiteMapper
{
    public long DeviceSiteMapperId { get; set; }

    public string FkSiteId { get; set; } = null!;

    public long FkDeviceRegistrationId { get; set; }
}
