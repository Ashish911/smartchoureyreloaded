using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class UserSiteInformation
{
    public long UserSiteId { get; set; }

    public string UserId { get; set; } = null!;

    public string SiteId { get; set; } = null!;

    public DateTime JoinedOn { get; set; }

    public bool? IsAdmin { get; set; }

    public bool? IsSubAdmin { get; set; }

    public string? UserAddedBy { get; set; }

    public virtual SiteInformation Site { get; set; } = null!;
}
