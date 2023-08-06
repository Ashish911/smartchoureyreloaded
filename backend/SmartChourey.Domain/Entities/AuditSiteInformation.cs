using System;
using System.Collections.Generic;

namespace SmartChourey.DAL;

public partial class AuditSiteInformation
{
    public long AuditSiteId { get; set; }

    public string SiteId { get; set; } = null!;

    public string SiteName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public bool? IsTrial { get; set; }

    public DateTime? TrialExpireOn { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? PeriodStart { get; set; }

    public string? PeriodEnd { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? BrowseTimeFrom { get; set; }

    public string? BrowseTimeTo { get; set; }

    public int? Gpsrange { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? ActivateOn { get; set; }

    public string? ActionDone { get; set; }

    public string QrcodeValue { get; set; } = null!;
}
