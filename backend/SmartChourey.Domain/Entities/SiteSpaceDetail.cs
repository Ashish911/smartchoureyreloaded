using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class SiteSpaceDetail
{
    public long SiteSpaceDetailId { get; set; }

    public string SiteId { get; set; } = null!;

    public float UsedSpaceInMb { get; set; }

    public int Ecategory { get; set; }

    public long CategoryId { get; set; }

    public long CategoryDetailId { get; set; }

    public int EuploadType { get; set; }

    public int Estatus { get; set; }
}
