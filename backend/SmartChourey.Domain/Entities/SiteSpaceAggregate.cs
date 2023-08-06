using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class SiteSpaceAggregate
{
    public long SiteSpaceUsedId { get; set; }

    public string SiteId { get; set; } = null!;

    public float UsedSpaceInMb { get; set; }

    public int AllocatedSpace { get; set; }

    public int Estatus { get; set; }
}
