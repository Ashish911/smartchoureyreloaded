using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class UserInformation
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? CompanyName { get; set; }

    public string? SiteName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? SiteId { get; set; }
}
