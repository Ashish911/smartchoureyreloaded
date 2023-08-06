using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class SiteDeclarationDeviceRegistrationMapper
{
    public long SiteDeclarationDeviceRegistrationMapperId { get; set; }

    public string DeviceUniqueId { get; set; } = null!;

    public long FkSiteDeclarationId { get; set; }

    public int? Estatus { get; set; }

    public DateTime? CreatedOn { get; set; }
}
