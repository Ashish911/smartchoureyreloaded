using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class SiteAccidentInformation
{
    public long AccidentId { get; set; }

    public string SiteId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<AccidentGalleryInformation> AccidentGalleryInformations { get; } = new List<AccidentGalleryInformation>();

    public virtual ICollection<AccidentPhotoInformation> AccidentPhotoInformations { get; } = new List<AccidentPhotoInformation>();

    public virtual SiteInformation Site { get; set; } = null!;
}
