using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class DeviceRegistration
{
    public long DeviceRegistrationId { get; set; }

    public string DeviceUniqueId { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int EdeviceType { get; set; }

    public string? FullName { get; set; }

    public string? CompanyName { get; set; }
}
