using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AuditUserSiteDeclarationInformation
{
    public long AuditUserSiteDeclarationId { get; set; }

    public long UserSiteDeclarationId { get; set; }

    public string UserId { get; set; } = null!;

    public long SiteDeclarationId { get; set; }

    public string SiteId { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public bool IsChecked { get; set; }

    public string ActionDone { get; set; } = null!;
}
