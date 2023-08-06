using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class SiteNewsInformation
{
    public long NewsId { get; set; }

    public string SiteId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<NewsGalleryInformation> NewsGalleryInformations { get; } = new List<NewsGalleryInformation>();

    public virtual ICollection<NewsPhotoInformation> NewsPhotoInformations { get; } = new List<NewsPhotoInformation>();

    public virtual SiteInformation Site { get; set; } = null!;
}
