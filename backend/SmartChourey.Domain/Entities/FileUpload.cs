using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class FileUpload
{
    public long FileUploadId { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public int Estatus { get; set; }

    public int EfileType { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public string? CustomName { get; set; }

    public string? ActualFileName { get; set; }

    public string? SiteId { get; set; }

    public int ViewMode { get; set; }

    public int? Ecategory { get; set; }

    public long? CategoryId { get; set; }
}
