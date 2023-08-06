using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class PublicUserboardPhotoInformation
{
    public long PhotoId { get; set; }

    public int PublicUserboardInformationId { get; set; }

    public string? PhotoName { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;
}
