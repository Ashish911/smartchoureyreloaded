using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class PublicUserboardInformation
{
    public int PublicUserboardId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }
}
