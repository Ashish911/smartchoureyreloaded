using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class SafetyGalleryInformation
{
    public long GalleryId { get; set; }

    public string GalleyName { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public string SiteId { get; set; } = null!;

    public long SafetyId { get; set; }

    public virtual SiteSafetyInformation Safety { get; set; } = null!;

    public virtual ICollection<SafetyPhotoInformation> SafetyPhotoInformations { get; } = new List<SafetyPhotoInformation>();

    public virtual SiteInformation Site { get; set; } = null!;
}
