using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class UserSiteDeclarationInformation
{
    public long UserSiteDeclarationId { get; set; }

    public string UserId { get; set; } = null!;

    public long SiteDeclarationId { get; set; }

    public string SiteId { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public bool IsChecked { get; set; }
}
