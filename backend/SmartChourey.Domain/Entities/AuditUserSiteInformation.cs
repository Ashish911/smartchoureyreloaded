using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AuditUserSiteInformation
{
    public long AuditUserSiteId { get; set; }

    public long UserSiteId { get; set; }

    public string UserId { get; set; } = null!;

    public string SiteId { get; set; } = null!;

    public DateTime JoinedOn { get; set; }

    public bool? IsAdmin { get; set; }

    public bool? IsSubAdmin { get; set; }

    public string ActionDone { get; set; } = null!;

    public string? UserAddedBy { get; set; }
}
