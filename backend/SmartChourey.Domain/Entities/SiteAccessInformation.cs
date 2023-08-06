using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class SiteAccessInformation
{
    public long SiteAccessId { get; set; }

    public string SiteId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string SiteAccessGivenTo { get; set; } = null!;

    public DateTime JoinedOn { get; set; }
}
