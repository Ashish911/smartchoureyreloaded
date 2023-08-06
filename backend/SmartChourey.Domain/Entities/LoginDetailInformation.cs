using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class LoginDetailInformation
{
    public long LoginId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? DeviceId { get; set; }

    public string? DeviceName { get; set; }
}
