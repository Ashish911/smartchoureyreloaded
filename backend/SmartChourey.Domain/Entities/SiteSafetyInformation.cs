using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class SiteSafetyInformation
{
    public long SafetyId { get; set; }

    public string SiteId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsDaily { get; set; }

    public bool? IsWeekly { get; set; }

    public bool? IsMonthly { get; set; }

    public virtual ICollection<SafetyGalleryInformation> SafetyGalleryInformations { get; } = new List<SafetyGalleryInformation>();

    public virtual ICollection<SafetyPhotoInformation> SafetyPhotoInformations { get; } = new List<SafetyPhotoInformation>();

    public virtual SiteInformation Site { get; set; } = null!;
}
