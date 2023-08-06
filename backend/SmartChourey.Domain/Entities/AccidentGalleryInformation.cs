using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AccidentGalleryInformation
{
    public long GalleryId { get; set; }

    public string GalleyName { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public string SiteId { get; set; } = null!;

    public long AccidentId { get; set; }

    public virtual SiteAccidentInformation Accident { get; set; } = null!;

    public virtual ICollection<AccidentPhotoInformation> AccidentPhotoInformations { get; } = new List<AccidentPhotoInformation>();

    public virtual SiteInformation Site { get; set; } = null!;
}
