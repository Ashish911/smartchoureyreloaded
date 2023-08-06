using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class ChoureyMediaComment
{
    public long ChoureyMediaCommentId { get; set; }

    public string Comment { get; set; } = null!;

    public int Ecategory { get; set; }

    public long ChoureyId { get; set; }

    public long ChoureyMediaId { get; set; }

    public int EuploadType { get; set; }

    public int EfileType { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public int EdeviceType { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }
}
